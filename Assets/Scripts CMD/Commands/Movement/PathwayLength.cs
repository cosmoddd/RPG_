using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathwayDraw))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PathwayRed))]

public class PathwayLength : MonoBehaviour {  // Live update of the distance between two points.

	public NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float distance;

	void Update () {

		if (navMeshAgent != null)
		{
	      distance = PathLength(navMeshAgent.path); 
		}
	}

    public void SetAgentSource(GameObject o)
    {
        navMeshAgent = o.GetComponent<NavMeshAgent>();
    }

     public float PathLength(NavMeshPath path)
        {
            if (path.corners.Length < 2)
                return 0;

            Vector3 previousCorner = path.corners[0];
            distance = 0.0F;
            int i = 1;
            while (i < path.corners.Length)
            {
                Vector3 currentCorner = path.corners[i];
                distance += Vector3.Distance(previousCorner, currentCorner);
                previousCorner = currentCorner;
                i++;
            }
            return distance;
        } 

}
