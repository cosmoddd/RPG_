using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	public GameObject hovered;
	public GameObject selected;

	IHoverable iHoverable;
	ISelectable iSelectable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Hover();

		if (Input.GetMouseButtonDown(0))
		{
			Select();
		}

	}

 	void Select(){
	 	
		if (CastRay()!= null)
		{
			if (CastRay().GetComponent<ISelectable>() != null)
				{
					iSelectable = CastRay().GetComponent<ISelectable>();
					iSelectable.Select();
				}
			}
		}
	
 

	void Hover(){
	{
		if (CastRay()!= null)
		{
			if (CastRay().GetComponent<IHoverable>() != null)
				{
					iHoverable = CastRay().GetComponent<IHoverable>();
					hovered = CastRay();
					iHoverable.HoverEnter();
				}
				else
				{
					if (iHoverable != null)
					{
						iHoverable.HoverExit();
					}
					iHoverable = null;
					hovered = null;	
				}
			}
		}
	}

	public GameObject CastRay(){

		Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
		{
			return interactionInfo.transform.gameObject;
		}

		else return null;

	}
}
