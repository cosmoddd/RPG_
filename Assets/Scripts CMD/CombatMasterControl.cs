using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CombatMasterControl : MonoBehaviour {

public delegate void CombatMasterControlDelegate (CombatMasterControl c);
public static event CombatMasterControlDelegate CombatMasterControlEvent;

public delegate void Command(); 
public static event Command ExecuteCommand;


public UnityEvent combatEventControl;
public List<GameObject> CharacterCombatants;


public GameObject zam;

	void Start () {

		StartCoroutine("Fetch");
	}

	IEnumerator Fetch()
	{
		yield return new WaitForSeconds(2f);
		
		if (CombatMasterControlEvent!= null)
		CombatMasterControlEvent(this);  // signals all combat controllers to join my event system
	}

	public void AddToEventControl(CombatController c)
	{
		CharacterCombatants.Add(c.gameObject);
	}

	public void Execute()
	{
		ExecuteCommand();
	}

}
