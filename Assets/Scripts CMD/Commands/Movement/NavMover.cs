using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMover : MonoBehaviour {

    public UnityEvent moveEvent;

    public delegate void NavWayDelegate ();
    public static event NavWayDelegate MoveComplete;

    public List<GameObject> navPointz;

    NavWaypointManager navWaypoints;
    
    public NavMeshAgent navMeshAgent;

    public void OnEnable()
    {
        
        navWaypoints = GetComponent<NavWaypointManager>();


    }

    public void Initialize()
    {

        print("Mover init");
        StartCoroutine ("MoveToWaypoint");
    }

    IEnumerator MoveToWaypoint()
    {

        navPointz = navWaypoints.navPointObjects;
        
        for (int i = 0; i < navPointz.Count; i++)
        {
            Vector3 destination = new Vector3(navPointz[i].transform.position.x,
                                                gameObject.transform.parent.position.y,
                                                navPointz[i].transform.position.z);
            yield return null;
            while ((gameObject.transform.parent.position != destination))
            {
                navMeshAgent.isStopped=false;
                yield return null;
                navMeshAgent.destination = destination;
                yield return null;
            }
            yield return null;
        }
        print("done moving");
        navMeshAgent.isStopped=false;
        moveEvent.Invoke();
        yield return null;


    }

}
