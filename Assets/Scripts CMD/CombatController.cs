using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// MASTER COMBAT TIMER FOR EACH CHARACTER

public class CombatController : MonoBehaviour, ISelectable
{

  	public delegate void SelectionDelegate();
	public static event SelectionDelegate DeSelect;

    public bool selected;

    public List<string> CommandQueue;
    public NavMeshAgent navMeshAgent;
    public GameObject thisCommand;
    public bool isPaused = false;
    public bool timerRunning = false;
    public int listLength = 10;

    void Start()
    {
        CombatController.DeSelect += _DeSelect;   // deselect this when another controller is clicked

        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        for (int i = 0; i < listLength; i++)
            {        
                CommandQueue.Add(null);
            }
    }

    public void Clicked()
    {   
        DeSelect();
        Invoke("SpawnMoveCommand", .01f);
        selected = true;
    }

    public void _DeSelect()
    {   if (selected)
        selected = false;
    }
    // Update is called once per frame
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


    void SpawnMoveCommand()
        {
            GameObject m;


            if (this.GetComponentInChildren<CommandMove>()==null)
            {     
                m = Instantiate(thisCommand, this.transform);      
                m.GetComponent<CommandMove>().controller = this;
                m.GetComponent<CommandMove>().navMeshAgent = navMeshAgent;
            }
            else
            { 
                print ("already attached!");
            }
            return;
         }

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

    private static WaitForSecondsRealtime HalfSecond()
    {
        return new WaitForSecondsRealtime(.5f);
    }

}
