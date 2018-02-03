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
    public Color combatColor;

    public List<string> CommandQueue;
    public NavMeshAgent navMeshAgent;

    public bool isPaused = false;
    public bool timerRunning = false;
    public int listLength = 10;

    void Start()
    {

        ResetName();

        CombatMasterControl.CombatMasterControlEvent += AddToEventControl;
        CombatMasterControl.ExecuteCommand += YammerBulls;

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
        print(this.name + " added to Master Combat Controller");
    }

    public void Select()
    {   

        if (SelectEvent == null)
        {
            print("select event = null");
            
            if (DeSelectAllEvent != null)           // deselect everything first
                DeSelectAllEvent();

        }

        else{                                        // -- OR --

            if (DeSelectAllEvent != null){          //deselect everything first
               DeSelectAllEvent();
            }
            SelectEvent();                          //then select
            SelectWithColor(combatColor);
        }

        selected = true;                        // set script as selected (debugging only(?))
    }

    public void DeSelect()
    {   if (selected)
        selected = false;
    }
    public void YammerBulls()
    {
        print ("YAMMER BULLS " + this.name);
    }

    private static WaitForSecondsRealtime HalfSecond()
    {
        return new WaitForSecondsRealtime(.5f);
    }

    void ResetName(){

        this.gameObject.name = string.Concat(this.transform.parent.name,this.gameObject.name);
    }

    void CommandSelector(){

    }

}