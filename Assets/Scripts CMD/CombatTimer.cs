using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTimer : MonoBehaviour
{

    public List<CombatCommand> command;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine("TimerExecute");
        }
    }

    IEnumerator TimerExecute()
    {
        for (int i = 0; i < 10; i++)
        {
            print(i+1);
			if (command[i] != null && command[i].commandString != null)
			{
           		 print(command[i].commandString);
			}
            yield return HalfSecond();
        }
        print("done");

    }

    private static WaitForSecondsRealtime HalfSecond()
    {
        return new WaitForSecondsRealtime(.5f);
    }
}
