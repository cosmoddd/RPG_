using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvailableCommands : MonoBehaviour {

public GameObject test;
public CombatCommandContainer testObject;

	void Start () {

/* 		if (test != null)
		{
			test.GetComponent<ICommandable>().SpawnCommand(this.gameObject);
		}	 */

		if (testObject != null)
		{
			print("Init on the scriptable object");
			testObject.Init(this.gameObject);
		}
	}

}
