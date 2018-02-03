using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvailableCommands : MonoBehaviour {

//public GameObject test;
public CombatCommandContainer testObject;

	public delegate void RouteToUI();
	public static event RouteToUI routeToUI;

	void Start () {

		CombatController c = GetComponent<CombatController>();	//	subscribe to combat selection command

		c.SelectWithColor += Select;

		if (testObject != null)
		{
			print("Init on the scriptable object");
			testObject.Init(this.gameObject);
		}  //go through a list
	}

	void Select(Color c)
	{
				// if object is selected
				// cycle through events and load button to UI
	//			testObject.commandButton.gameObject.transform.SetParent(combatGrid);
	
	}



}
