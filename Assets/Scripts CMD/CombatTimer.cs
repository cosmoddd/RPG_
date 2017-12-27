using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MASTER COMBAT TIMER FOR EACH CHARACTER

public class CombatTimer : MonoBehaviour
{

    public List<string> CommandList;
    public GameObject thisCommand;
    public bool isPaused = false;
    public bool timerRunning = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J) && !timerRunning)
        {
            StartCoroutine("TimerExecute");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject m;
            if (this.GetComponentInChildren<CombatCommandMove>()==null)
            {           
                m = Instantiate(thisCommand, this.transform);
                m.GetComponent<CombatCommandMove>().timer = this;
            }
            else{ print ("already attached!");}
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }
        
    }

    IEnumerator TimerExecute()
    {   
        timerRunning = true;
        while(isPaused) {yield return null;}

        CommandExecute c = this.gameObject.AddComponent<CommandExecute>();
        for (int i = 0; i < 10; i++)
        {
              while(isPaused) {yield return null;}

			if (CommandList[i] != null && CommandList[i] != null)
			{
                c.CommandSwitch();
           		print(CommandList[i]);
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
