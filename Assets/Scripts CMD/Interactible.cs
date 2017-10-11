using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour {

    [HideInInspector]
	public UnityEngine.AI.NavMeshAgent playerAgentFunky;
	private bool hasInteracted;

	public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent playerAgentFunky)
	{
		hasInteracted = false;
		this.playerAgentFunky = playerAgentFunky;
		playerAgentFunky.stoppingDistance = 3f;
		playerAgentFunky.destination = this.transform.position;	

	}

   

	void Update()
	{
		if (!hasInteracted && playerAgentFunky != null && !playerAgentFunky.pathPending) {
		
			if (playerAgentFunky.remainingDistance <= playerAgentFunky.stoppingDistance)
			{
				Interact ();
				hasInteracted = true;
			}

		}
	}

	public virtual void Interact()
	{
		Debug.Log ("overriding with base class, classy.");
	}

}
