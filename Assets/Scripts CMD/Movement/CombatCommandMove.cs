using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommandMove : CombatCommand  // the master controller for the 'Move' combat command.
{

    public delegate void CombatCommandMoveDelegate ();
    public static event CombatCommandMoveDelegate MovingEvent;

    NavPointCreator navPointCreator;
    public List<Vector3> navPoint;
    public List<GameObject> navPointObject;

    public override void Start()
    {
        transform.localPosition = new Vector3(0,0,0);
        navPointCreator = (NavPointCreator)gameObject.GetComponent<NavPointCreator>();
        navPointCreator.DistanceSet += RoundDistance;
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            navPointCreator.SetNav();
            navPoint.Add(navPointCreator.point1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.G)&& (transform.GetComponent<NavWayPointMover>() == null)) // check if it's not already attached
        {
                MovingEvent();
        //        WayPointMover(navPoint);  // move the parent along the waypoints
        }
    }

    void RoundDistance(float d, Vector3 p)
    {
        slots = Mathf.RoundToInt(d);
        print("Distance Rounded= " + slots);
        AddToList();
        return;
    }

    public void UnSubscribe()
    {
        navPointCreator.DistanceSet -= RoundDistance;
    }

    void WayPointMover(List<Vector3> navPoint)
    {
/* 
			NavWayPointMover n = this.gameObject.AddComponent<NavWayPointMover>();
            n.navPoint = navPoint;
            n.StartCoroutine("MoveToWaypoint");
 */
    }

}
