using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// MASTER COMBAT TIMER FOR EACH CHARACTER

public class CombatController : MonoBehaviour, ISelectable

{
  	public delegate void SelectionDelegate();
  	public event SelectionDelegate SelectEvent;
	public static event SelectionDelegate DeSelectAllEvent;

    public delegate void SelectionWithColor(Color c);
    public event SelectionWithColor SelectWithColor;

    public bool selected;
    public Color combatantColor;

    public List<CombatCommand> CommandQueue;
    public NavMeshAgent navMeshAgent;

    public bool isPaused = false;
    public bool timerRunning = false;
    public int listLength = 10;

    CombatCommand mostRecentCommand;

    void Start()
    {

        ResetName();

        CombatMasterControl.CombatMasterControlEvent += AddToEventControl;
   
        CombatController.DeSelectAllEvent += DeSelect;
        NavWaypoint.DeSelectAllEvent += DeSelect;

        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        for (int i = 0; i < listLength; i++)
            {        
                CommandQueue.Add(null);
            }
    }

    void AddToEventControl(CombatMasterControl combatMasterControl)
    {
        combatMasterControl.AddToEventControl(this);
    }

    public void Select()
    {   

        if (SelectEvent == null)
        {           
            if (DeSelectAllEvent != null)           // deselect everything first
                DeSelectAllEvent();

        }

        else{                                        // -- OR --

            if (DeSelectAllEvent != null){          //deselect everything first
               DeSelectAllEvent();
            }
            SelectEvent();                          //then select
            this.gameObject.AddComponent<CombatCommandsToUI>().combatCommandsToUI(this, combatantColor);       //select with color (to send to UI)
        }

        selected = true;                        // set script as selected (debugging only(?))
    }

    public void DeSelect()
    {   if (selected)
        selected = false;
    }

    private static WaitForSecondsRealtime HalfSecond()
    {
        return new WaitForSecondsRealtime(.5f);
    }

    void ResetName(){

        this.gameObject.name = string.Concat(this.transform.parent.name,this.gameObject.name);
    }

    void CommandSelector(){

      mostRecentCommand = CommandQueue[CommandQueue.Count - 1];

    }

}