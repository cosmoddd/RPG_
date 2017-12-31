using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWayPointMover : MonoBehaviour {

    public List<Vector3> navPoint;
    public bool isPaused = false;
    
    public NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    IEnumerator MoveToWaypoint()
    {

        for (int i = 0; i < navPoint.Count; i++)
        {
            Vector3 targetPosition = new Vector3(navPoint[i].x,gameObject.transform.parent.position.y,navPoint[i].z);
            Vector3 _direction = (targetPosition - gameObject.transform.parent.position).normalized; //find the vector pointing from our position to the target    
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);  //create the rotation we need to be in to look at the target

            while ((Math.Round(gameObject.transform.parent.rotation.eulerAngles.y, 1) != Math.Round(_lookRotation.eulerAngles.y, 1)))           
            {
                gameObject.transform.parent.rotation = Quaternion.Slerp(gameObject.transform.parent.rotation, _lookRotation, Time.deltaTime * 3);
                while (isPaused)
                {
                    yield return null;
                }
                yield return null;
            }
            print("go");
            while ((gameObject.transform.parent.position != targetPosition))
            {

                gameObject.transform.parent.position = Vector3.MoveTowards(gameObject.transform.parent.position,
                                                                            targetPosition,
                                                                            (5 * Time.deltaTime));

                while (isPaused)
                {
                    yield return null;
                }

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

        IEnumerator MoveToWaypointDNU()
    {

        for (int i = 0; i < navPoint.Count; i++)
        {
            Vector3 targetPosition = new Vector3(navPoint[i].x,gameObject.transform.parent.position.y,navPoint[i].z);
            Vector3 _direction = (targetPosition - gameObject.transform.parent.position).normalized; //find the vector pointing from our position to the target    
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);  //create the rotation we need to be in to look at the target

            while ((Math.Round(gameObject.transform.parent.rotation.eulerAngles.y, 1) != Math.Round(_lookRotation.eulerAngles.y, 1)))           
            {
                gameObject.transform.parent.rotation = Quaternion.Slerp(gameObject.transform.parent.rotation, _lookRotation, Time.deltaTime * 3);
                while (isPaused)
                {
                    yield return null;
                }
                yield return null;
            }
            print("go");
            while ((gameObject.transform.parent.position != targetPosition))
            {

                gameObject.transform.parent.position = Vector3.MoveTowards(gameObject.transform.parent.position,
                                                                            targetPosition,
                                                                            (5 * Time.deltaTime));

                while (isPaused)
                {
                    yield return null;
                }

                yield return null;
            }
            yield return null;
        }
        print("done moving");
        yield return null;
        Destroy(this);
		//delete script
    }

}
