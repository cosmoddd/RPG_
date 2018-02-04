using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvailableCommands : MonoBehaviour {

	public List <CombatCommandContainer> availableCombatCommands;

	Color combatantColor;
	UnityAction ButtonClicked;

	void Start () {

		CombatController combatController = GetComponent<CombatController>();	//	subscribe to combat selection command

		if (availableCombatCommands != null)
		{

			for (int i = 0; i < availableCombatCommands.Count; i++)
			{
				availableCombatCommands[i].Init(this.gameObject);
			}  
		}
	}

}
