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

    public delegate void MoveDelegate();
    public static event MoveDelegate Move;

    public NavMeshAgent navMeshAgent;

    public bool ready = true;
    public bool deselected = false;

    public DistanceCalc distanceCalc;
    public DistanceCompare distanceCompare;

    void OnEnable()
    {
        Selection.MouseOver += DeSelected;  
        Selection.MouseExit += DeSelectedFalse; 

        NavWaypoint.WayPointHover += DeSelected;
        NavWaypoint.WayPointHoverExit += DeSelectedFalse;


        NavWaypoint.WayPointClicked += Ready;

//        NavWaypointMover.MoveComplete += Ready;
    }

    public override void Start()
    {
        distanceCalc = GetComponent<DistanceCalc>();
        distanceCompare = GetComponent<DistanceCompare>();
        transform.localPosition = new Vector3(0, 0, 0);
        ready = true;
    }


    public override void Update()
    {

        while(deselected == true)
        {
            return;
        }

        distanceCompare.DistanceTest(this, distanceCalc);

        if (Input.GetMouseButtonDown(0) && GetComponentInChildren<NavWaypointMover>() == null && distanceCompare.InRange == true && ready)  // Left Click
        {
            Clicked(GetThePoint.PickVector3());  //send point out to all relevant scripts
            return;
        }

        if (Input.GetMouseButtonDown(1) && GetComponentInChildren<NavWaypointMover>() == null)  // Right Click
        {
            RightClicked(GetThePoint.PickVector3());
            if (ready)
            {   
                distanceCalc.currentDistance = 0;
                distanceCalc.ready = false;
                ready = false;
                return;
            }
            if (!ready)
            {
                distanceCalc.currentDistance = 0;
                distanceCalc.ready = true;
                ready = true;
                return;
            }
            return;
        }

        #region go script
        // --------------v a separate class
        if (Input.GetKeyDown(KeyCode.G) && (transform.GetComponent<NavWaypointMover>() == null)) // check if it's not already attached
        {
            if (transform.parent.GetComponentInChildren<PathwayDraw>().gameObject.activeInHierarchy == true)
            {
                transform.parent.GetComponentInChildren<PathwayDraw>().gameObject.SetActive(false);
            }
            navMeshAgent = this.GetComponentInParent<NavMeshAgent>();
            NavWaypointMover m = this.gameObject.AddComponent<NavWaypointMover>();
            m.navMeshAgent = navMeshAgent;
            m.Initialize();
            Move();  // execute the move event
            return;
        }
        //  -------------^ a separate class
        #endregion
    }

    public void OverWaypoint()
    {
        ready = false;
    }

    public void DeSelected()
    {
        deselected = true;
    }

     public void DeSelectedFalse()
    {
        deselected = false;
    }


    public void Ready()
    {
        NavWaypointMover.MoveComplete -= Ready;
        Invoke("DelayedReady", .1f);
    }

    void DelayedReady()
    {
        ready = true;
        this.enabled = true;
    }

    void OnDisable()
    {
        NavWaypointMover.MoveComplete -= Ready;
    }

    void OnDestroy()
    {

        NavWaypoint.WayPointHover -= DeSelectedFalse\;
        NavWaypoint.WayPointClicked -= Ready;
    }

}
