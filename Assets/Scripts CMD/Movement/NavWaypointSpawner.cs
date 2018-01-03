using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointSpawner : MonoBehaviour {  //SPAWNS WAYPOINTS


public void Start(){
    CombatCommandMove.Move += DisableNavAgent;
    NavWaypointMover.MoveComplete += DestroySelf;
}

void DisableNavAgent()
{
    print("navmesh disabledd");
    CombatCommandMove.Move -= DisableNavAgent;
    GetComponent<NavMeshAgent>().enabled = false;
}

void DestroySelf()
{
      NavWaypointMover.MoveComplete -= DestroySelf;
      Destroy(gameObject);
}
}
