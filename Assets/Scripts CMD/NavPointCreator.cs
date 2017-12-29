using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointCreator : MonoBehaviour {

	public delegate void GetDistance(float d, Vector3 p);
	public event GetDistance DistanceSet;

	float pointDistance;
	public Vector3 point1, point2;
	bool startingPoint;

	// Use this for initialization
	void Start () {
		
		startingPoint = true;

	}
	

	public void SetNav()
		{	
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity))
			if (startingPoint == true)
			{
				point1 = interactionInfo.point;
				startingPoint = false;
				return;
			}
			if (startingPoint == false) 
			{
				point2 = interactionInfo.point;
				pointDistance = Vector3.Distance(point1, point2);
				DistanceSet(pointDistance, point2);  // event for calling distance and setting it
				point1 = point2;
				return;
			}

		}

}

