using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RoomNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NavMeshSurface nav = GetComponent<NavMeshSurface>();
        nav.BuildNavMesh();
    }
}
