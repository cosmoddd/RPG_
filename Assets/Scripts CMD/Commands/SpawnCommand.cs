using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpawnCommand : MonoBehaviour, ICommandable {

    GameObject m;

	public void spawnCommand(GameObject host, string s)
	{
       
        m = Instantiate(this.gameObject, host.transform);  //instantiates this command prefab

        m.GetComponent<CombatCommand>().combatController = this.gameObject.GetComponent<CombatController>();   //Assign combat controller script

        m.GetComponent<CombatCommand>().name = s;           //assign name of command
        m.GetComponent<CombatCommand>().commandName = s;    //assign name of command to name variable

        Destroy(m.GetComponent<SpawnCommand>());  //Destroy this spawning helper script since it is only used for instantiation
	}


}
