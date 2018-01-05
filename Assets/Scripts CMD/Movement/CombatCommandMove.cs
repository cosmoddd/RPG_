using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatCommandMove : CombatCommand  // the master controller for the 'Move' combat command.
{
    public delegate void SetPointDelegate(Vector3 point);
    public event SetPointDelegate Clicked;
    public event SetPointDelegate RightClicked;

    public delegate void MoveDelegate();
    public static event MoveDelegate Move;
    public NavMeshAgent navMeshAgent;
    public bool maxDistanceExceeded = false;
    public bool ready = true;
    bool moving = false;


    public override void Start()
    {
        NavWaypoint.WayPointClicked += Ready;
        NavWaypointMover.MoveComplete += Ready;
        transform.localPosition = new Vector3(0, 0, 0);
        ready = true;
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponentInChildren<NavWaypointMover>() == null && !maxDistanceExceeded && ready)// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            Clicked(GetThePoint.PickVector3());  //send point out to all relevant scripts
            return;
        }

        if (Input.GetMouseButtonDown(1) && GetComponentInChildren<NavWaypointMover>() == null)// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            ready = false;
            RightClicked(GetThePoint.PickVector3());
            return;
        }

        if (Input.GetKeyDown(KeyCode.G) && (transform.GetComponent<NavWaypointMover>() == null)) // check if it's not already attached
        {
            if (transform.parent.GetComponentInChildren<DrawPathway>().gameObject.activeInHierarchy == true)
            {
                transform.parent.GetComponentInChildren<DrawPathway>().gameObject.SetActive(false);
                return;
            }
            navMeshAgent = this.GetComponentInParent<NavMeshAgent>();
            NavWaypointMover m = this.gameObject.AddComponent<NavWaypointMover>();
            m.navMeshAgent = navMeshAgent;
            m.Initialize();
            Move();  // execute the move event
            return;
        }
    }

    public void Ready()
    {
        print("Readeeey");
        Invoke("DelayedReady", .1f);
    }

    void DelayedReady()
    {
        ready = true;
    }

}
