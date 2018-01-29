using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class   CombatCommand : MonoBehaviour {

	public CombatController combatController;
	public bool selected;

	public virtual void Start()
	{
		combatController = GetComponentInParent<CombatController>();
		
		combatController.SelectEvent += _SelectEvent;   
		CombatController.DeSelectAllEvent += _DeSelectEvent;   

	}

    public void _SelectEvent()
    {
		print("selected" + this.name);
        selected = true;
    }

     public virtual void _DeSelectEvent()
    {   
		if (selected)
        selected = false;
    }

	virtual public void OnDisable(){

		combatController.SelectEvent -= _SelectEvent;   
		CombatController.DeSelectAllEvent -= _DeSelectEvent;   
	}

}
