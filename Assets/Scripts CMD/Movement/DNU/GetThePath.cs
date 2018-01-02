using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetThePath : MonoBehaviour { //this needs to be called 

	// Use this for initialization
	public static NavMeshPath CalculatePath(GameObject source, Vector3 destination) {
		
		NavMeshAgent agent = source.GetComponent<NavMeshAgent>();
		agent.isStopped = true;
       agent.SetDestination(destination); //create the path pointing to target 
 	   return agent.path;
 	
	}

	IEnumerator Path()
	{
		yield return null;
	}
}
