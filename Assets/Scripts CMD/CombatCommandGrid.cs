using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommandGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		AvailableCommands.ClearUI += ClearUI;
		AvailableCommands.SendCommandToUI += AddCommandsToUI;
	}

	void ClearUI(GameObject o)
	{
		print("clearing UI");
		foreach (Transform child in transform) 
		{
  	    	GameObject.Destroy(child.gameObject);
		}
	}

	void AddCommandsToUI(GameObject o)
	{
		o.transform.SetParent(this.transform);
	}


	void Update () {
		
	}
}
