using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathwayDraw : MonoBehaviour  // this class should deal specifically with the drawing of the lines between waypoints
{  //DRAWS WAYPOINTS

    public NavMeshAgent navMeshAgent;
    NavMeshPath path;//
    LineRenderer line;

    public Vector3[] pathway;
    public float lineOffset = 1;
    public float distance;

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

    }

}


