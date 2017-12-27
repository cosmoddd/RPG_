using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent funkyPlayerAgent;

	void Start()
	{
		funkyPlayerAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	void Update()
	{
	
		if (Input.GetMouseButtonDown(0) && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
			{
				GetInteraction();

			}		
	}


	void GetInteraction()
	{
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity))
			{
				GameObject interactedObject = interactionInfo.collider.gameObject;
				if (interactedObject.tag == "Interactiblez")
				{
					interactedObject.GetComponent<Interactible>().MoveToInteraction (funkyPlayerAgent);
				}

				else
				{
					funkyPlayerAgent.destination = interactionInfo.point;
					funkyPlayerAgent.stoppingDistance = 0;
				}
			}
		}
			
}
	
