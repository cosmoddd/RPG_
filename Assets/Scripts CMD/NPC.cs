using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactible {

    public string[] dialog;
    public string NameO;

	 public override void Interact()
	{
        DialogSystem.Instance.AddNewDialog(dialog, NameO);
            Debug.Log ("interacting with NPC");
	}


}
