using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CombatCommandContainer : ScriptableObject {

    public string commandName;
    public GameObject commandPrefab;

    public void Init(GameObject o)
    {
        commandPrefab.GetComponent<ICommandable>().SpawnCommand(o);
    }

}


