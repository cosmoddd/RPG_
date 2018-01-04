using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatCommandMove : CombatCommand  // the master controller for the 'Move' combat command.
{
    public delegate void SetPointDelegate(Vector3 point);
    public event SetPointDelegate Clicked;

    public delegate void MoveDelegate();
    public static event MoveDelegate Move;
    public NavMeshAgent navMeshAgent;
    public bool maxDistanceExceeded = false;

    bool moving = false;



    public override void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);

    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponentInChildren<NavWaypointMover>() == null && !maxDistanceExceeded)// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            Clicked(GetThePoint.PickVector3());  //send point out to all relevant scripts
        }

        if (Input.GetKeyDown(KeyCode.G) && (transform.GetComponent<NavWaypointMover>() == null)) // check if it's not already attached
        {
            transform.parent.GetComponentInChildren<DrawPathway>().gameObject.SetActive(false);
            navMeshAgent = this.GetComponentInParent<NavMeshAgent>();
            NavWaypointMover m = this.gameObject.AddComponent<NavWaypointMover>();
            m.navMeshAgent = navMeshAgent;
            m.Initialize();
            Move();  // execute the move event
        }


    }
}
