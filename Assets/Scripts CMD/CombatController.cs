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

    public bool selected;

    public List<string> CommandQueue;
    public NavMeshAgent navMeshAgent;
    public GameObject thisCommand;
    public bool isPaused = false;
    public bool timerRunning = false;
    public int listLength = 10;

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
        print(this.name + " added to Master Combat Controller");
    }

    public void Select()
    {   

        if (SelectEvent == null)
        {
            if (DeSelectAllEvent != null)           // deselect everything first
                DeSelectAllEvent();

            Invoke("SpawnCommandMove", .01f);          // then spawn the command
        }

        else{                                        // -- OR --

            if (DeSelectAllEvent != null){          //deselect everything first
               DeSelectAllEvent();
            }
            SelectEvent();                          //then select
        }

        selected = true;                        // set script as selected (debugging only(?))
    }

    public void DeSelect()
    {   if (selected)
        selected = false;
    }

     void SpawnCommandMove()
        {
            GameObject m;
            if (this.GetComponentInChildren<CommandMove>()==null)
            {     
                m = Instantiate(thisCommand, this.transform);      
                m.GetComponent<CommandMove>().combatController = this;
                m.GetComponent<CommandMove>().navMeshAgent = navMeshAgent;
            }
            else
            { 
                print ("already attached!");
            }
            return;
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

}