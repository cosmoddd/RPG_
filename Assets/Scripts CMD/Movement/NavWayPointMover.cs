using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWayPointMover : MonoBehaviour {

    public List<Vector3> navPoint;
    public bool isPaused = false;
    
    public NavMeshAgent daAgent;

    void Start()
    {
        CombatCommandMove.MovingEvent += Initialize;
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
        //    Vector3 _direction = (targetPosition - gameObject.transform.parent.position).normalized; //find the vector pointing from our position to the target    
        //    Quaternion _lookRotation = Quaternion.LookRotation(_direction);  //create the rotation we need to be in to look at the target

            print("go");
            while ((gameObject.transform.parent.position != destination))
            {
                daAgent.destination = destination;
                yield return null;
            }
            yield return null;
        }
        print("done moving");
        yield return null;
        Destroy(this);
		//delete script
    }
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
                isPaused = !isPaused;        
        }
    }


}
