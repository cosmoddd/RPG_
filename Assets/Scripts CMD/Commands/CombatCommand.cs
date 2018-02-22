using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class   CombatCommand : MonoBehaviour {

	public CombatController combatController;
	//public bool selected;
	public string commandName;
	
	public virtual void Start()
	{
		combatController = GetComponentInParent<CombatController>();
		
		combatController.SelectEvent += _SelectEvent;   
		CombatController.DeSelectAllEvent += _DeSelectEvent;   
	}

    public void _SelectEvent()
    {
    }

    public virtual void _DeSelectEvent()
    {   
    }

	public virtual void _ButtonClick()
	{
	}

	public virtual void RemoveCommand()
	{
	}

	public virtual void OnDisable(){

		combatController.SelectEvent -= _SelectEvent;   
		CombatController.DeSelectAllEvent -= _DeSelectEvent;   
	}

}
