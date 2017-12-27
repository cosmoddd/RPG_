using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommand : MonoBehaviour {


	string commandString;
	public int slots;
	public CombatTimer timer;

	public virtual void Start()
	{
	
	}

	public void AddToList()
	{
		int i = NullString();
		int j = i+slots;
		for (i =NullString(); i < j; i++)
		{
			if (i < (timer.CommandList.Count) && (i > -1))
			{
			timer.CommandList[i] = this.ToString();
			}
		}
	}


	public virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) && (NullString()!=-1))
        {
			AddToList();
        }

	}

	int NullString()
	{
		return timer.CommandList.IndexOf("");
	}

}
