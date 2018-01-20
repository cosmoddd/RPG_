using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypoint : MonoBehaviour, ISelectable
{  //SPAWNS WAYPOINTS

    public delegate void NavWaypointDelegate();
    public event NavWaypointDelegate WayPointClicked;
    public static event NavWaypointDelegate WayPointHover;
    public static event NavWaypointDelegate WayPointHoverExit;
    
    public static event NavWaypointDelegate DeSelectAllEvent;

    public GameObject spawner;
    CommandMove commandMove;


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
        WayPointClicked -= commandMove.Ready;
        Destroy(gameObject);
    }

    public void Select()
    {
        Destroy(GetComponent<BoxCollider>());
        if (DeSelectAllEvent != null)
        DeSelectAllEvent();      
        if (WayPointClicked != null)
        {
            WayPointClicked(); 
        } 
    }

    public void DeSelect()
    {
        
    }

    public void SetupDependency(GameObject o)
    {
        print("dependency set");
        spawner = o;
        commandMove = o.GetComponent<CommandMove>();
        WayPointClicked += commandMove.Ready;
        return;
    }

    void OnMouseOver()
    {
        if (WayPointHover != null)
        {
             WayPointHover();
        }
    }

    void OnMouseExit()
    {
        if (WayPointHover != null)
        {
             WayPointHoverExit();
        }
    }

}
