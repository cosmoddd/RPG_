using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypoint : MonoBehaviour {  //SPAWNS WAYPOINTS

public delegate void NavWaypointDelegate();
public static event NavWaypointDelegate WayPointClicked;

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

void OnMouseDown()
    {

        print ("get ready");
        WayPointClicked();
    }

}
