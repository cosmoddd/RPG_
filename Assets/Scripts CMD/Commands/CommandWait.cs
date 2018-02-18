using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWait : CombatCommand {

	public int WaitTime;
	
	public override void _ButtonClick()
	{
		AddPause();
	}


	public void AddPause(){  				//add pause command to command queue

		for (int i = 0; i < combatController.CommandQueue.Count; i++)
			
			if (combatController.CommandQueue[i] == null && 
				(combatController.CommandQueue.Count - combatController.CommandQueue.IndexOf(combatController.CommandQueue[i])) >= WaitTime)
				{
					for (int j = 0; j < WaitTime; j++)
					{
						combatController.CommandQueue.Insert(i, this);
						i++;
					}
					combatController.ResetListLength();
					return;
				}			
	}
}
