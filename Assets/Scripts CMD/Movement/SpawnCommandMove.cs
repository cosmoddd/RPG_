using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpawnCommandMove : MonoBehaviour, ICommandable {

    GameObject m;

	public void SpawnCommand(GameObject host)
	{
        m = Instantiate(this.gameObject, host.transform);      
        m.GetComponent<CommandMove>().combatController = this.gameObject.GetComponent<CombatController>();
        m.GetComponent<CommandMove>().navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();

        Destroy(m.GetComponent<SpawnCommandMove>());
	}


}
