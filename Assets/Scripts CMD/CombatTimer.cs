using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// MASTER COMBAT TIMER FOR EACH CHARACTER

public class CombatTimer : MonoBehaviour
{

    public List<string> CommandQueue;
    public NavMeshAgent navMeshAgent;
    public GameObject thisCommand;
    public bool isPaused = false;
    public bool timerRunning = false;
    public int listLength = 10;
    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        for (int i = 0; i < listLength; i++)
            {        
                CommandQueue.Add(null);
            }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J) && !timerRunning)
        {
            StartCoroutine("TimerExecute");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }
        

    }

    void OnMouseDown()
    {       
        Invoke("SpawnMoveCommand", .01f);
    }

    void SpawnMoveCommand()
        {
            GameObject m;


            if (this.GetComponentInChildren<CombatCommandMove>()==null)
            {     
                m = Instantiate(thisCommand, this.transform);      
                m.GetComponent<CombatCommandMove>().timer = this;
                m.GetComponent<CombatCommandMove>().navMeshAgent = navMeshAgent;
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
