using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointCreator : MonoBehaviour {

	public delegate void GetDistance(float d, Vector3 p);
	public event GetDistance DistanceSet;

	public GameObject NavPoint;
	public List<GameObject> NavPointPool;

	float pointDistance;
	public Vector3 point1, point2;
	bool startingPoint;

	void Start () {
		
		startingPoint = true;
		NavWayPointMover.MoveComplete += NavPointsDestroy;

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
				NavPointPlace(interactionInfo.point);
				return;
			}
			if (startingPoint == false) 
			{
				point2 = interactionInfo.point;
				pointDistance = Vector3.Distance(point1, point2);
				DistanceSet(pointDistance, point2);  // event for calling distance and setting it
				point1 = point2;
				NavPointPlace(interactionInfo.point);
				return;
			}

		}

	
	public void NavPointPlace(Vector3 point)
	{

		GameObject o = Object.Instantiate(NavPoint, new Vector3(point.x, (point.y+1), point.z), Quaternion.identity);

		NavWaypointRenderer navRender = o.GetComponent<NavWaypointRenderer>();

		if (NavPointPool.Count != 0)
		{
			navRender.target = NavPointPool[(NavPointPool.Count-1)].transform;  		// tell object to point to last waypoint
		}

		NavPointPool.Add(o);

		return;
	}

	void NavPointsDestroy()
	{
		for (int i = 0; i < NavPointPool.Count; i++)
		{
			GameObject.Destroy(NavPointPool[i]);
		}
		NavPointPool.Clear();
	}
	


}

