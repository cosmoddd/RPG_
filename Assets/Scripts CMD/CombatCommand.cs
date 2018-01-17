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
		combatController.DeSelectEvent += _DeSelectEvent;   

		selected = true;

	}

    public void _SelectEvent()
    {
		print("selected?");
        selected = true;
    }

     public void _DeSelectEvent()
    {   if (selected)
        selected = false;
    }


	public void OnDisable(){

		combatController.SelectEvent -= _SelectEvent;   
		combatController.DeSelectEvent -= _DeSelectEvent;   
	}

}
