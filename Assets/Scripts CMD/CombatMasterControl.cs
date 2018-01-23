using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CombatMasterControl : MonoBehaviour {

public delegate void CombatMasterControlDelegate (CombatMasterControl c);
public static event CombatMasterControlDelegate CombatMasterControlEvent;


public UnityEvent combatEventControl;
public List<GameObject> CharacterCombatants;


public GameObject zam;

	// Use this for initialization
	void Start () {

		StartCoroutine("Fetch");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.anyKeyDown)
		{
			combatEventControl.Invoke();
		}

	}

	IEnumerator Fetch()
	{
		print("starting combat...");

		yield return new WaitForSeconds(2f);
		
		if (CombatMasterControlEvent!= null)
		CombatMasterControlEvent(this);  // signals all combat controllers to join my event system
	}

	public void AddToEventControl(CombatController c)
	{
		combatEventControl.AddListener(c.YammerBulls);
		CharacterCombatants.Add(c.gameObject);
	}


}
