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
    CombatController combatController;


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
        WayPointClicked -= combatController.Select;
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
        spawner = o;
        combatController = o.GetComponentInParent<CombatController>();
        WayPointClicked += combatController.Select;
        return;
    }

}
