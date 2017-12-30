using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour {


// need to merge path / line renderer with navigation thing.


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


//    getPath();
}

void Update(){


    line.SetPosition(0, new Vector3 (transform.position.x, (transform.position.y - agent.baseOffset + (lineOffset - lineOffsetInit)), transform.position.z)); //set the line's origin

    agent.SetDestination(target.position); //create the path

    DrawPath(agent.path);

	lengthSoFar = PathLength(agent.path);

	pathway = agent.path.corners;

    agent.isStopped = true;//add this if you don't want to move the agent
}

void DrawPath(NavMeshPath path){
    if(path.corners.Length < 2) //if the path has 1 or no corners, there is no need
        return;

    line.positionCount = path.corners.Length; //set the array of positions to the amount of corners

    for(int i = 1; i < path.corners.Length; i++){

		line.SetPosition(i, new Vector3(path.corners[i].x,
										((path.corners[i].y)+(lineOffset)),
										path.corners[i].z));
    }

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
    }

}