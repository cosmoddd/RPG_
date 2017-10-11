using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPostAction : ActionItem {

    public string[] dialog;

	public override void Interact()
	{
		base.Interact ();
        DialogSystem.Instance.AddNewDialog(dialog, "Sign");
		Debug.Log ("also a hot dog.");

	}

}
