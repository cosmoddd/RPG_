using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommandMove : CombatCommand {

	NavPointCreator nav;
	public bool moving;
	public List<Vector3> navPoints;	

	public override void Start()
	{
		nav = (NavPointCreator)gameObject.AddComponent<NavPointCreator>();
		nav.DistanceSet += RoundDistance; 
	}


	
public void	MoveToWaypoint(){}
	/*

		move to next waypoint
		when reached, get next waypoint
		keep moving until last waypoint reached

		animate figure while doing it

//  when not moving, stop and save position



	*/

	public override void Update()
	{
			if (Input.GetMouseButtonDown(0))// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
			{
				nav.SetNav();
				return;
			}	

			if (moving == true)
			{
				//get current position
				MoveToWaypoint();
			}
	}

	void RoundDistance(float d, Vector3 p)
	{
		slots = Mathf.RoundToInt(d);
		navPoints.Add(p);
		print("Distance Rounded= " + slots);	
		AddToList();
		return;
	}

}
