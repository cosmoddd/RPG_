using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactible {

	public override void Interact()
	{
		Debug.Log ("interacting with item");
		playerAgentFunky.stoppingDistance = 1.8f;
	}
		
}
