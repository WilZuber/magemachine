using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    private enum WallType { wall, empty, door };
    private enum RoomType { standard, start, exit, challenge };
    private enum Decoration { };

    public static int level;
    public GameObject[] prefabs;
    private static Quaternion[] rotations;
    private static GameObject[] emptyRoom;
    private static GameObject[] wall;
    private static GameObject[] door;
    private static GameObject[] corner;
    private static GameObject levelExit;
    private static readonly float mergeChance = 0.5f; //chance for connected rooms to be merged when applicable

    private static Cell[,] cells;
    private static List<Cell> standardCells;
    private static int radius, diameter;

    // Directions: NESW
    private static readonly int[][] nextDirs = new[] { new[] { 0, 1, 3 }, new[] { 0, 1, 2 }, new[] { 1, 2, 3 }, new[] { 0, 2, 3 } }; // [0,3] \ incoming
    private static readonly int[] incoming = new int[] { 2, 3, 0, 1 }; //(n+2) % 4
    private static readonly int[] dx = new int[] { 0, 1, 0, -1 };
    private static readonly int[] dy = new int[] { -1, 0, 1, 0 };

    private class Cell
    {
        public WallType[] sides = new WallType[4];
        public RoomType type;
        private readonly int R, C;
        private static readonly float size = 8;
        public Transform room;
        public int firstDirection; // input direction when the room was created

        public Cell(int x, int y, int dir, RoomType type)
        {
            this.type = type;
            firstDirection = dir;
            C = x;
            R = y;
            cells[R, C] = this;
            standardCells.Add(this);
            foreach (int nextDir in nextDirs[dir])
                sides[nextDir] = WallType.wall;
        }

        //the adjacent cell in the given direction
        public Cell CellOnSide(int dir)
        {
            return cells[R + dy[dir], C + dx[dir]];
        }

        public void SetSide(int dir, WallType wallType)
        {
            sides[dir] = wallType;
            Cell otherCell = CellOnSide(dir);
            if (otherCell != null)
            {
                otherCell.sides[incoming[dir]] = wallType;
            }
        }

        public void Output()
        {
            Vector3 position = new((C - radius) * size, 0, -(R - radius) * size);
            room = PlaceRoom(position, this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        //level -> radius: 1 -> 2, 2-3 -> 3, 4-6 -> 4, 7-10 -> 5, etc
        radius = Mathf.CeilToInt(Mathf.Sqrt(2 * level + 0.25f) + 0.5f);
        diameter = 2 * radius + 1;
        Generate();
    }

    private void Generate()
    {
        int startDir = Random.Range(0, 4);
        bool retry;
        do
        {
            cells = new Cell[diameter, diameter];
            standardCells = new();

            Cell startCell = new(radius, radius, startDir, RoomType.start);
            standardCells.Clear(); //don't do anything else with the starting room
            startCell.SetSide(incoming[startDir], WallType.wall);
            Generate(radius, radius, startDir, radius);

            //ensure there are at least 4r non-starting rooms (probably change to something else later)
            retry = standardCells.Count < 4 * radius;
        } while (retry);

        AssignRooms();
        Output();
    }

    //in: direction of new room from previous room (0123 -> NESW)
    private void Generate(int x, int y, int dir, int remaining)
    {
        Cell currentCell = cells[y, x];

        foreach (int nextDir in nextDirs[dir])
        {
            if (Random.Range(0f, radius) < remaining)
            { //whether to proceed in this direction (linear falloff chance)

                int newX = x + dx[nextDir];
                int newY = y + dy[nextDir];

                if (cells[newY, newX] == null)
                { //room has not already been generated
                    new Cell(newX, newY, nextDir, RoomType.standard);
                    currentCell.SetSide(nextDir, WallType.door);
                    Generate(newX, newY, nextDir, remaining - 1);
                }
                else
                {
                    Cell otherCell = cells[newY, newX];
                    if (otherCell.type == RoomType.standard) //don't connect to starting room
                        currentCell.SetSide(nextDir, WallType.door);
                }
            }
        }
    }

    private List<Cell> CellsNextToStart()
    {
        List<Cell> adjCells = new(3);
        Cell startCell = cells[radius, radius];
        foreach (int dir in nextDirs[startCell.firstDirection])
        {
            adjCells.Add(startCell.CellOnSide(dir));
        }
        return adjCells;
    }

    private void AssignRooms()
    {
        //this prevents special rooms adjacent to the starting point
        foreach (Cell cell in CellsNextToStart())
        {
            standardCells.Remove(cell);
        }

        //place the exit
        GetStandardCell().type = RoomType.exit;

        // [r-1, 2(r-1)] challenge rooms
        //int challengeRooms = r-1 + (int) (Math.random() * r);
        int challengeRooms = 1 + standardCells.Count / 6 + Random.Range(0, 3);
        for (int i = 0; i < challengeRooms; i++)
        {
            GetStandardCell().type = RoomType.challenge;
        }

        //allow start-adjacent rooms to merge
        foreach (Cell cell in CellsNextToStart())
        {
            standardCells.Add(cell);
        }

        foreach (Cell cell in standardCells)
        {
            //NE (only need to process one direction of each axis)
            for (int dir = 0; dir < 2; dir++)
            {
                //if there is a door to another standard cell
                if (cell.sides[dir] == WallType.door && cell.CellOnSide(dir).type == RoomType.standard)
                {
                    //whether to merge the rooms
                    if (Random.value < mergeChance)
                    {
                        cell.sides[dir] = WallType.empty;
                        cell.CellOnSide(dir).sides[incoming[dir]] = WallType.empty;
                    }
                }
            }
        }

    }

    //return a random standard cell and remove it from the list
    private Cell GetStandardCell()
    {
        //random index
        int index = Random.Range(0, standardCells.Count);
        Cell cell = standardCells[index];
        standardCells.RemoveAt(index);
        return cell;
    }

    private void Output()
    {
        foreach (Cell cell in cells)
        {
            cell?.Output();
        }
        /*GameObject floor = cells[radius, radius].room.GetChild(0).gameObject;
        //floor.GetComponent<NavMeshSurface>().BuildNavMesh();
        NavMeshBuildSettings settings = NavMesh.CreateSettings();
        NavMeshSurface nav = floor.GetComponent<NavMeshSurface>();
        NavMesh.AddNavMeshData(nav.navMeshData);*/
        /*foreach (Cell cell in cells)
        {
            if (cell != null)
            {
                int index = cell.type == RoomType.challenge ? 5 : 0;
                Transform room = cell.room;
                GameObject floor = cell.room.GetChild(index).gameObject;
                //floor.GetComponent<NavMeshSurface>().BuildNavMesh();
                NavMeshSurface nav = floor.GetComponent<NavMeshSurface>();
                nav.BuildNavMesh();
                //NavMesh.AddNavMeshData(nav.navMeshData, room.position, room.rotation);
            }
        }*/
        level++;
    }

    public void Initialize()
    {
        emptyRoom = new[] { prefabs[0], prefabs[4] };
        wall = new[] { prefabs[1], prefabs[5] };
        door = new[] { prefabs[2], prefabs[6] };
        corner = new[] { prefabs[3], prefabs[7] };
        levelExit = prefabs[8];
        rotations = new Quaternion[4];
        for (int i = 0; i < 4; i++)
        {
            rotations[i] = Quaternion.Euler(0, 90 * i, 0);
        }
        LevelExit.currentLevel = 3;
    }

    private static Transform PlaceRoom(Vector3 position, Cell cell)
    {
        int environment = cell.type == RoomType.challenge ? 1 : 0;

        //room
        Transform room = Instantiate(emptyRoom[environment], position, Quaternion.identity).transform;

        for (int i = 0; i < 4; i++)
        {
            int side0 = i;
            int side1 = (i + 1) % 4;

            //walls
            switch (cell.sides[i])
            {
                case WallType.wall: Instantiate(wall[environment], position, rotations[i], room); break;
                case WallType.door: Instantiate(door[environment], position, rotations[i], room); break;
            }

            //corners
            //if (wallTypes[side0] != WallType.empty || wallTypes[side1] != WallType.empty) {
            Instantiate(corner[environment], position, rotations[i], room);
            //}
        }

        if (cell.type == RoomType.exit)
        {
            Instantiate(levelExit, position, rotations[cell.firstDirection], room);
        }

        return room;
    }
}
