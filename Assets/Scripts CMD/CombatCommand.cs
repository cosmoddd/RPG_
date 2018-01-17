using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class   CombatCommand : MonoBehaviour {

	public CombatController combatController;
	public bool selected = true;

    public delegate void MoveDelegate();
    public static event MoveDelegate Move;

	public virtual void Start()
	{

		combatController = GetComponentInParent<CombatController>();
		
		combatController.SelectEvent += _SelectEvent;   
		CombatController.DeSelectAllEvent += _DeSelectEvent;   

		selected = true;

	}

    public void _SelectEvent()
    {
		print("selected?");
        selected = true;
    }

     public void _DeSelectEvent()
    {   
		if (selected)
		print("deselect " + this.transform.name);
        selected = false;
    }


	public void OnDisable(){

		combatController.SelectEvent -= _SelectEvent;   
		CombatController.DeSelectAllEvent -= _DeSelectEvent;   
	}

}
