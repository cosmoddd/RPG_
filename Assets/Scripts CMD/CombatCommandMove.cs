using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatCommandMove : CombatCommand
{
    NavPointCreator nav;
    public List<Vector3> navPoint;

/*     public bool isPaused = false;
    public bool moving = false; */

    public override void Start()
    {
        nav = (NavPointCreator)gameObject.AddComponent<NavPointCreator>();
        nav.DistanceSet += RoundDistance;
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            nav.SetNav();
            navPoint.Add(nav.point1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.G)&& (transform.GetComponent<NavWayPointMover>() == null)) // check if it's not already attached
        {
                WayPointMover(navPoint);  // move the parent along the waypoints
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
        nav.DistanceSet -= RoundDistance;
    }

    void WayPointMover(List<Vector3> navPoint)
    {
			NavWayPointMover n = this.gameObject.AddComponent<NavWayPointMover>();
            n.navPoint = navPoint;
            n.StartCoroutine("MoveToWaypoint");
    }

}
