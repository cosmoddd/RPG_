using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathwayLength : MonoBehaviour {

	public NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float distance;

    public float lengthSoFar;

	void Update () {

		if (navMeshAgent != null)
		{
	      distance = PathLength(navMeshAgent.path);  // could be a separate class
		}
	}

    public void SetAgentSource(GameObject o)
    {
        navMeshAgent = transform.parent.GetComponentInChildren<PathwayDraw>().navMeshAgent;
    }

     public float PathLength(NavMeshPath path)
        {
            if (path.corners.Length < 2)
                return 0;

            Vector3 previousCorner = path.corners[0];
            lengthSoFar = 0.0F;
            int i = 1;
            while (i < path.corners.Length)
            {
                Vector3 currentCorner = path.corners[i];
                lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
                previousCorner = currentCorner;
                i++;
            }
            return lengthSoFar;
        } 

}
