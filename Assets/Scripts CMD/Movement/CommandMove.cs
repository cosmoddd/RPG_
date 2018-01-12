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

    public DistanceCalc distanceCalc;
    public DistanceCompare distanceCompare;

    public override void Start()
    {
        distanceCalc = GetComponent<DistanceCalc>();
        distanceCompare = GetComponent<DistanceCompare>();
        transform.localPosition = new Vector3(0, 0, 0);
        ready = true;
    }

    void OnEnable()
    {
        NavWaypoint.WayPointClicked += Ready;
        NavWaypoint.WayPointHover += Unready;
        NavWaypointMover.MoveComplete += Ready;
    }

    public override void Update()
    {
        distanceCompare.DistanceTest(this, distanceCalc);

        if (Input.GetMouseButtonDown(0) && GetComponentInChildren<NavWaypointMover>() == null && distanceCompare.InRange == true && ready)  // Left Click
        {
            Clicked(GetThePoint.PickVector3());  //send point out to all relevant scripts
            return;
        }

        if (Input.GetMouseButtonDown(1) && GetComponentInChildren<NavWaypointMover>() == null)  // Right Click
        {
            if (ready)
            {
                distanceCalc.enabled = false;
            }
            if (!ready)
            {
                distanceCalc.enabled = true;
            }
            ready = false;
            RightClicked(GetThePoint.PickVector3());
            return;
        }

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
    }

    public void Unready()
    {
        ready = false;
    }

    public void Ready()
    {
        distanceCalc.enabled = true;
        NavWaypointMover.MoveComplete -= Ready;
        Invoke("DelayedReady", .1f);
    }

    void DelayedReady()
    {
        ready = true;
    }

    void OnDisable()
    {
        NavWaypoint.WayPointClicked -= Ready;
        NavWaypoint.WayPointHover -= Unready;
        NavWaypointMover.MoveComplete -= Ready;
    }

}
