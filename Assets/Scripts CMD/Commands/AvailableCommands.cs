using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvailableCommands : MonoBehaviour {

	public List <CombatCommandContainer> availableCombatCommands;

	public delegate void SendUICommand(GameObject o);
	public static event SendUICommand ClearUI;
	public static event SendUICommand SendCommandToUI;
	
	Color combatantColor;
	UnityAction ButtonClicked;

	void Start () {

		CombatController combatController = GetComponent<CombatController>();	//	subscribe to combat selection command
		combatController.SelectWithColor += selectWithColorToUI;
		combatantColor = combatController.combatantColor;

		if (availableCombatCommands != null)
		{

			for (int i = 0; i < availableCombatCommands.Count; i++)
			{
				availableCombatCommands[i].Init(this.gameObject);
			}  
		}
	}

	void selectWithColorToUI(Color c)
	{
		ClearUI(null);

		for (int i = 0; i < availableCombatCommands.Count; i++)

			{
				GameObject b = Instantiate(availableCombatCommands[i].commandButton);
				Button button = b.GetComponent<Button>();	//inherit button properties from Scriptable Object
				ColorBlock cb = button.colors;   			// 
				cb.normalColor = combatantColor;			//inherity UI Color
				button.colors = cb;							//

				button.onClick.AddListener(availableCombatCommands[i].commandPrefab.GetComponent<CombatCommand>()._SelectEvent);

				SendCommandToUI(b);
			}  

	}



}
