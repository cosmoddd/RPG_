using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavWaypointManager))]
[RequireComponent(typeof(DistanceCalc))]
[RequireComponent(typeof(DistanceCompare))]

public class CommandMove : CombatCommand  // the master controller for the 'Move' combat command.
{
    public delegate void SetPointDelegate(Vector3 point);
    
    public event SetPointDelegate Clicked;
    public event SetPointDelegate RightClicked;

    public NavMeshAgent navMeshAgent;
    public DistanceCalc distanceCalc;
    public DistanceCompare distanceCompare;

    public bool canPlaceWaypoint = true;

    void OnEnable()
    {
        NavWaypoint.WayPointClicked += Ready;
//        NavWaypointMover.MoveComplete += Ready;
    }

    public override void Start()
    {
        base.Start();

        distanceCalc = GetComponent<DistanceCalc>();
        distanceCompare = GetComponent<DistanceCompare>();
        transform.localPosition = new Vector3(0, 0, 0);
        canPlaceWaypoint = true;
    }


    public void Update()
    {

        while(!selected)
        {
            return;
        }

        distanceCompare.DistanceTest(this, distanceCalc);

        if (Input.GetMouseButtonDown(0) && GetComponentInChildren<NavWaypointMover>() == null && distanceCompare.InRange == true && canPlaceWaypoint)  // Left Click
        {
            Clicked(GetThePoint.PickVector3());  //send point out to all relevant scripts
            return;
        }

        if (Input.GetMouseButtonDown(1) && GetComponentInChildren<NavWaypointMover>() == null)  // Right Click
        {
            RightClicked(GetThePoint.PickVector3());
            if (canPlaceWaypoint)
            {   
                distanceCalc.currentDistance = 0;
                distanceCalc.ready = false;
                canPlaceWaypoint = false;
                return;
            }
            if (!canPlaceWaypoint)
            {
                distanceCalc.currentDistance = 0;
                distanceCalc.ready = true;
                canPlaceWaypoint = true;
                return;
            }
            return;
        }

    }

    public void OverWaypoint()
    {
        canPlaceWaypoint = false;
    }

    public void Ready()
    {
        NavWaypointMover.MoveComplete -= Ready;
        Invoke("DelayedReady", .1f);
    }

    void DelayedReady()
    {
        canPlaceWaypoint = true;
        this.enabled = true;
    }

    void OnDisable()
    {
        base.OnDisable();
        NavWaypointMover.MoveComplete -= Ready;
    }

    void OnDestroy()
    {   
        combatController.selected = false;
        NavWaypoint.WayPointClicked -= Ready;
    }

}
