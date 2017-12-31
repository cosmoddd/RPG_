using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointRenderer : MonoBehaviour {

LineRenderer line; //to hold the line Renderer
public Transform target; //to hold the transform of the target
NavMeshAgent agent; //to hold the agent of this gameObject

public float lengthSoFar;

public Vector3[] pathway;

public float lineOffset = 1;

public void Start(){

    CombatCommandMove.Move += DisableNavMesh;

    line = GetComponent<LineRenderer>(); //get the line renderer
    agent = GetComponent<NavMeshAgent>(); //get the agent
	line.useWorldSpace = true;
    agent.isStopped = true;//add this if you don't want to move the agent
    StartCoroutine ("DrawPath");
}

IEnumerator DrawPath()
{
    if (target != null)
    {
        agent.SetDestination(target.position); //create the path pointing to target
        yield return null;                      // this yield is important.  do it!
    }
    pathway = agent.path.corners;
    line.positionCount = pathway.Length;

    for(int i = 0; i < pathway.Length; i++){
		line.SetPosition(i, new Vector3(pathway[i].x,((pathway[i].y)+(lineOffset)),pathway[i].z));
    }
    yield return null;
}

void DestroySelf()
{
      CombatCommandMove.Move -= DisableNavMesh;
}

void DisableNavMesh()
{
    CombatCommandMove.Move -= DisableNavMesh;
    agent.enabled = false;
}


}
