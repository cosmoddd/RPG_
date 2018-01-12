using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypoint : MonoBehaviour
{  //SPAWNS WAYPOINTS

    public delegate void NavWaypointDelegate();
    public static event NavWaypointDelegate WayPointClicked;
    public static event NavWaypointDelegate WayPointHover;

    public void Start()
    {
        CommandMove.Move += DisableNavAgent;
        NavWaypointMover.MoveComplete += DestroySelf;
    }

    void DisableNavAgent()
    {
        CommandMove.Move -= DisableNavAgent;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    void DestroySelf()
    {
        CommandMove.Move -= DisableNavAgent;
        NavWaypointMover.MoveComplete -= DestroySelf;
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        WayPointClicked();
    }

    void OnMouseOver()
    {
        WayPointHover();
    }

}
