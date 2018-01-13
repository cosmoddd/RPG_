using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

	public delegate void SelectionEvent();
	public static event SelectionEvent MouseOver;
	public static event SelectionEvent MouseExit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void  OnMouseOver()
	{
		MouseOver();
	}

	void OnMouseExit()
	{
		MouseExit();
	}
}
