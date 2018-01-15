using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {


	public GameObject hovered;
	public GameObject selected;
	ISelectable iselectable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Selection();

		if (Input.GetMouseButtonDown(0))
		{
			if (iselectable != null)
			{
				iselectable.Clicked();
				selected = hovered;
			}
		}

	}

	void Selection(){
	{
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                if (interactionInfo.transform.gameObject.GetComponent<ISelectable>() != null)
                {
                    hovered = interactionInfo.transform.gameObject;
                    iselectable = interactionInfo.transform.gameObject.GetComponent<ISelectable>();
                }
                else
                {
					selected = null;
				}
            }

    }

	}
}
