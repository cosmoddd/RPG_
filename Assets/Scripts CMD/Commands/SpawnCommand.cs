using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpawnCommand : MonoBehaviour, ICommandable {

    GameObject m;

	public void spawnCommand(GameObject host)
	{
       
        m = Instantiate(this.gameObject, host.transform);      

        m.GetComponent<CombatCommand>().combatController = this.gameObject.GetComponent<CombatController>();

        Destroy(m.GetComponent<SpawnCommand>());
	}


}
