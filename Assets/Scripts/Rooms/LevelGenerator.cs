using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private enum WallType {wall, empty, door};
    private enum RoomType {standard, start, exit, challenge};
    private enum Decoration {};

    public static int level;
    public GameObject prefab;
    public static GameObject staticPrefab;
    private static readonly float mergeChance = 0.5f; //chance for connected rooms to be merged when applicable

    private static Cell[,] cells;
    private static List<Cell> standardCells;
    private static int r, d;

    // Directions: NESW
    private static readonly int[][] nextDirs = new[]{new[]{0, 1, 3}, new[]{0, 1, 2}, new[]{1, 2, 3}, new[]{0, 2, 3}}; // [0,3] \ incoming
    private static readonly int[] incoming = new int[]{2, 3, 0, 1}; //(n+2) % 4
    private static readonly int[] dx = new int[]{0, 1, 0, -1};
    private static readonly int[] dy = new int[]{-1, 0, 1, 0};

    private class Cell {
        public WallType[] sides = new WallType[4];
        public RoomType type;
        private int x, y;
        private static readonly float size = 8;
        
        public Cell(int x, int y, int dir, RoomType type) {
            this.type = type;
            this.x = x;
            this.y = y;
            cells[this.y, this.x] = this;
            standardCells.Add(this);
            foreach (int nextDir in nextDirs[dir])
                sides[nextDir] = WallType.wall;
        }
        
        //the adjacent cell in the given direction
        public Cell CellOnSide(int dir) {
            return cells[y + dy[dir], x + dx[dir]];
        }
        
        public void SetSide(int dir, WallType wallType) {
            sides[dir] = wallType;
            Cell otherCell = CellOnSide(dir);
            if (otherCell != null) {
                otherCell.sides[incoming[dir]] = wallType;
            }
        }

        public void Output() {
            Vector3 position = new((x - r) * size, 0, (y - r) * size);
            Instantiate(staticPrefab, position, Quaternion.identity);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        staticPrefab = prefab;
        //level -> radius: 1 -> 2, 2-3 -> 3, 4-6 -> 4, 7-10 -> 5, etc
        r = Mathf.CeilToInt(Mathf.Sqrt(2*level + 0.25f) + 0.5f);
        d = 2*r + 1;
        Generate();
    }

    private void Generate() {
        int startDir = Random.Range(0, 4);
        bool retry;
        do {
            cells = new Cell[d, d];
            standardCells = new();

            Cell startCell = new(r, r, startDir, RoomType.start);
            standardCells.Clear(); //don't do anything else with the starting room
            startCell.SetSide(incoming[startDir], WallType.wall);
            Generate(r, r, startDir, r);
            
            //ensure there are at least 4r non-starting rooms (probably change to something else later)
            retry = standardCells.Count < 4*r;
        } while (retry);
        
        AssignRooms(startDir);
        Output();
    }
    
    //in: direction of new room from previous room (0123 -> NESW)
    private void Generate(int x, int y, int dir, int remaining) {
                                //updateX(x);
                                //updateY(y);
        Cell currentCell = cells[y, x];
        
        foreach (int nextDir in nextDirs[dir]) {
            if (Random.Range(0f, r) < remaining) { //whether to proceed in this direction (linear falloff chance)
                
                int newX = x + dx[nextDir];
                int newY = y + dy[nextDir];
                
                if (cells[newY, newX] == null) { //room has not already been generated
                    new Cell(newX, newY, nextDir, RoomType.standard);
                    currentCell.SetSide(nextDir, WallType.door);
                    Generate(newX, newY, nextDir, remaining - 1);
                } else {
                    Cell otherCell = cells[newY, newX];
                    if (otherCell.type == RoomType.standard) //don't connect to starting room
                        currentCell.SetSide(nextDir, WallType.door);
                }
            }
        }
    }
    
    private List<Cell> CellsNextToStart(int startDir) {
        List<Cell> adjCells = new(3);
        Cell startCell = cells[r, r];
        foreach (int dir in nextDirs[startDir]) {
            adjCells.Add(startCell.CellOnSide(dir));
        }
        return adjCells;
    }
    
    private void AssignRooms(int startDir) {
        //this prevents special rooms adjacent to the starting point
        foreach (Cell cell in CellsNextToStart(startDir))
        {
            standardCells.Remove(cell);
        }
        
        //place the exit
        GetStandardCell().type = RoomType.exit;
        
        // [r-1, 2(r-1)] challenge rooms
        //int challengeRooms = r-1 + (int) (Math.random() * r);
        int challengeRooms = 1 + standardCells.Count/6 + Random.Range(0, 3);
        for (int i = 0; i < challengeRooms; i++) {
            GetStandardCell().type = RoomType.challenge;
        }
        
        //allow start-adjacent rooms to merge
        foreach (Cell cell in CellsNextToStart(startDir))
        {
        standardCells.Add(cell);
        }
        
        foreach (Cell cell in standardCells) {
            //NE (only need to process one direction of each axis)
            for (int dir = 0; dir < 2; dir++) {
                //if there is a door to another standard cell
                if (cell.sides[dir] == WallType.door && cell.CellOnSide(dir).type == RoomType.standard) {
                    //whether to merge the rooms
                    if (Random.value < mergeChance) {
                        cell.sides[dir] = WallType.empty;
                        cell.CellOnSide(dir).sides[incoming[dir]] = WallType.empty;
                    }
                }
            }
        }
        
    }
    
    //return a random standard cell and remove it from the list
    private Cell GetStandardCell() {
        //random index
        int index = Random.Range(0, standardCells.Count);
        Cell cell = standardCells[index];
        standardCells.RemoveAt(index);
        return cell;
    }

    private void Output() {
        foreach (Cell cell in cells)
        {
            cell?.Output();
        }
    }
}
