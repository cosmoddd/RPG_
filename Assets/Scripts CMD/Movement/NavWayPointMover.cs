using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWayPointMover : MonoBehaviour {

    public delegate void NavWayDelegate ();
    public static event NavWayDelegate MoveComplete;

    public List<Vector3> navPoint;
    public bool isPaused = false;
    public NavMeshAgent daAgent;

    void Start()
    {




    }

    void Initialize()
    {
        StartCoroutine ("MoveToWaypoint");
    }

    IEnumerator MoveToWaypoint()
    {
        daAgent = GetComponentInParent<NavMeshAgent>();
        for (int i = 0; i < navPoint.Count; i++)
        {
            Vector3 destination = new Vector3(navPoint[i].x,gameObject.transform.parent.position.y,navPoint[i].z);
            print("go");
            while ((gameObject.transform.parent.position != destination))
            {
                daAgent.destination = destination;
                yield return null;
            }
            yield return null;
        }
        print("done moving");
        MoveComplete();
        yield return null;

        Destroy(this);

    }
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
                isPaused = !isPaused;        
        }
    }


}
