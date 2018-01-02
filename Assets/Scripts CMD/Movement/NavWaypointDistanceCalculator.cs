using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointDistanceCalculator : MonoBehaviour {



	NavMeshAgent agent;
	NavMeshPath path;

	void Start () {
		
		CombatCommandMove combatTimer = GetComponentInParent<CombatCommandMove>();
		agent = GetComponentInParent<NavMeshAgent>();

	}
	

	private float calculatePathDistance(NavMeshPath path)
	{
		float distance = .0f;
		for (var i = 0; i < path.corners.Length -1; i++)
		{
			distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
		}
		return distance;
	}

	void CalculateDistance(){

		// get distance between two points
		// calculate their path along nav mesh agent path
		// round it to single unit segmants
		// upstream those segments to the Combat Command Queue

	}

}
