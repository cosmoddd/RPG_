using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawPathway : MonoBehaviour  // this class should deal specifically with the drawing of the lines between waypoints
{  //DRAWS WAYPOINTS

    public NavMeshAgent navMeshAgent;
    NavMeshPath path;//
    LineRenderer line;

    public Vector3[] pathway;
    public float lineOffset = 1;
    public float distance;

    public float lengthSoFar;

    void Start()
    {
        line = GetComponent<LineRenderer>(); //get the line renderer
        this.transform.parent.GetComponentInChildren<CalculateTotalDistance>().drawPathway = this;
        navMeshAgent.isStopped = true;
    }

    public void SetAgentSource(GameObject o)
    {
        navMeshAgent = o.GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        Vector3 targetPoint = GetThePoint.PickVector3();
        if (targetPoint != Vector3.zero)
        {
            navMeshAgent.SetDestination(targetPoint); //create the path pointing to target
        }
        pathway = navMeshAgent.path.corners;
        line.positionCount = pathway.Length;

        for (int i = 0; i < pathway.Length; i++)
        {
            line.SetPosition(i, new Vector3(pathway[i].x, ((pathway[i].y) + (lineOffset)), pathway[i].z));
        }

      distance = PathLength(navMeshAgent.path);  // could be a separate class
    }

// could be a separate class  ----v

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

// could be a separate class  -----^
}


