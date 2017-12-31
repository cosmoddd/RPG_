using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWayPathRenderer : MonoBehaviour {

LineRenderer line; //to hold the line Renderer
public Transform target; //to hold the transform of the target
NavMeshAgent agent; //to hold the agent of this gameObject

public float lengthSoFar;

public Vector3[] pathway;

public float lineOffset = 1;
float lineOffsetInit;

public void Start(){

    line = GetComponent<LineRenderer>(); //get the line renderer
    agent = GetComponent<NavMeshAgent>(); //get the agent

	line.useWorldSpace = true;
	lineOffsetInit = lineOffset;
    DrawPath(agent.path);

 //  getPath();
}


void DrawPath(NavMeshPath path){

    if (target != null)
    {
        agent.SetDestination(target.position); //create the path
    }

    agent.isStopped = true;//add this if you don't want to move the agent

 /*    if(path.corners.Length < 2) //if the path has 1 or no corners, there is no need
        return;
 */
//set the array of positions to the amount of corners
    }

void Update(){

	pathway = agent.path.corners;
    line.positionCount = pathway.Length;
    line.SetPosition(0, new Vector3 (transform.position.x, pathway[0].y+lineOffset, transform.position.z)); // base line
    for(int i = 1; i < pathway.Length; i++){
		line.SetPosition(i, new Vector3(pathway[i].x,((pathway[i].y)+(lineOffset)),pathway[i].z));
    }
}
/*

	}

	float PathLength(NavMeshPath path) {
        if (path.corners.Length < 2)
            return 0;
        
        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length) {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
    } */

}
