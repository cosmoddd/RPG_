using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour, IHoverable {

	public delegate void SelectionEvent ();
	public static event SelectionEvent Enter;
	public static event SelectionEvent Exit;

	public bool hovering;

	public void  HoverEnter()
	{
		if (Enter != null)
		{
			hovering = true;
			Enter();
		}
	}

	public void HoverExit()
	{
		if (Exit != null)
		{
			hovering = false;
				Exit();
		}
	}
}
