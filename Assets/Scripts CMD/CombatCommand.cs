using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommand : MonoBehaviour {


	public string commandString;
	public int slots;
	public CombatTimer timer;

	void Start()
	{
	
	}

	void AddToList()
	{
		for (int i = 0; i < slots; i++)
		{
			
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
        {
			AddToList();
        }

	}



}
