using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDelete : MonoBehaviour {

public CombatController combatControllerSelected;
Button deleteButton;

void Start()
{
    deleteButton = GetComponent<Button>();
    CombatCommandsToUI.SendToDeleteButton += AddTheCommand;
}

void AddTheCommand(GameObject o)
{
    combatControllerSelected = o.GetComponent<CombatController>();

    ColorBlock cb1 = deleteButton.colors;                          //inherit UI Color
    cb1.normalColor = combatControllerSelected.combatantColor;            //inherit UI Color pt 2
    deleteButton.colors = cb1;   	                                //inherit UI color pt 3

}

void RevealTheThing(){

    print(combatControllerSelected.transform.name);

}

public void RemoveCommand()
{
    CombatCommand combatCommand;
	for (int i = (combatControllerSelected.CommandQueue.Count - 1); i >= 0; i--)
    {
        if (combatControllerSelected.CommandQueue[i] != null)
        {
            combatCommand = combatControllerSelected.CommandQueue[i];
            combatCommand.RemoveCommand();
            return;
        }
    }
}

}
