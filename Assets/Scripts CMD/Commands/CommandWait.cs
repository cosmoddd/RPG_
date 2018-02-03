using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWait : CombatCommand {

	public int WaitTime;

	void Update () {
		
		if (Input.GetKeyDown(KeyCode.W) && combatController.selected)
		{
			AddPause();
		}
	}

	public void AddPause(){

		for (int i = 0; i < WaitTime; i++)
		{
			combatController.CommandQueue.Add("Wait "+ i);
		}

	}

}
