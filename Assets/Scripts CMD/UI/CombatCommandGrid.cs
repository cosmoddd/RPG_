using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatCommandGrid : MonoBehaviour {

	public Button deleteButton;

	// Use this for initialization
	void Start () {
		
		CombatCommandsToUI.ClearUI += ClearUI;
		CombatCommandsToUI.SendCommandToUI += AddCommandsToUI;
	}

	void ClearUI(GameObject o)
	{
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
