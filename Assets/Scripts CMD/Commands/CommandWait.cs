using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWait : CombatCommand {

	public int WaitTime;
	
	public override void _ButtonClick()
	{
		AddPause();
	}

	public void AddPause(){

		print("adding pause command");
		for (int i = 0; i < WaitTime; i++)
		{
			combatController.CommandQueue.Add(this);
		}

	}

}
