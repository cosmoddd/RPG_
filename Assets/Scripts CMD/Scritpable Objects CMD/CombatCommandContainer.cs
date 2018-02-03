using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CombatCommandContainer : ScriptableObject {

    public string commandName;
    public GameObject commandPrefab;
    public Button commandButton;

    public void Init(GameObject o)
    {
        commandPrefab.GetComponent<ICommandable>().SpawnCommand(o);
    }

}


