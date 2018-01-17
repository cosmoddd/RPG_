using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// MASTER COMBAT TIMER FOR EACH CHARACTER

public class CombatController : MonoBehaviour, ISelectable
{

  	public delegate void SelectionDelegate();
  	public event SelectionDelegate SelectEvent;
	public event SelectionDelegate DeSelectEvent;

    public bool selected;

    public List<string> CommandQueue;
    public NavMeshAgent navMeshAgent;
    public GameObject thisCommand;
    public bool isPaused = false;
    public bool timerRunning = false;
    public int listLength = 10;

    void Start()
    {

        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        for (int i = 0; i < listLength; i++)
            {        
                CommandQueue.Add(null);
            }
    }

    public void Select()
    {   

        if (SelectEvent == null)
        {
            Invoke("SpawnCommandMove", .01f);
        }
        else{   
            SelectEvent();
        }
        selected = true;
    }

    public void DeSelect()
    {   if (selected)
        selected = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J) && !timerRunning)
        {
            StartCoroutine("TimerExecute");
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            return;
        }
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

    //----v could be separate class
#region timerExecute
    IEnumerator TimerExecute()
    {   
        timerRunning = true;
        while(isPaused) {yield return null;}

        CommandExecute c = this.gameObject.AddComponent<CommandExecute>();
        for (int i = 0; i < 10; i++)
        {
              while(isPaused) {yield return null;}

			if (CommandQueue[i] != null && CommandQueue[i] != null)
			{
                c.CommandSwitch();
           		print(CommandQueue[i]);
			}
              while(isPaused) {yield return null;}

                   yield return HalfSecond();

              while(isPaused) {yield return null;}
        }
        
        Destroy (c);
        print("done");
        timerRunning = false;
        yield return null;

    }
#endregion
    //----^ could be separate class

    private static WaitForSecondsRealtime HalfSecond()
    {
        return new WaitForSecondsRealtime(.5f);
    }

}