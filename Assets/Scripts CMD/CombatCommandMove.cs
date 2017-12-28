using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommandMove : CombatCommand {

	NavPointCreator nav;
	public bool moving;
	public List<Vector3> navPoint;	


	public override void Start()
	{

		nav = (NavPointCreator)gameObject.AddComponent<NavPointCreator>();
		nav.DistanceSet += RoundDistance; 
	}


	
 IEnumerator MoveToWaypoint(){

	 	Transform parent;
		parent = this.transform.parent; 
		print("Moving to waypoint");
			parent.position =  Vector3.MoveTowards(parent.position, navPoint[0], (3* Time.deltaTime));
			yield return new WaitForSeconds(3);
}





	public override void Update()
	{
			if (Input.GetMouseButtonDown(0))// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
			{
				nav.SetNav();
				navPoint.Add(nav.point1);
				return;
			}	

			if (moving == true)
			{
				//get current position
				MoveToWaypoint();
			}

			if (Input.GetKeyDown(KeyCode.G))
			{
				StartCoroutine("MoveToWaypoint");
			}
	}

	void RoundDistance(float d, Vector3 p)
	{
		slots = Mathf.RoundToInt(d);
		print("Distance Rounded= " + slots);	
		AddToList();
		return;
	}

}
