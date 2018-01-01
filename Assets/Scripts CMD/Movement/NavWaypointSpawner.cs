using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWaypointSpawner : MonoBehaviour {

	public GameObject navPointPrefab;
	NavWaypointChooser navWaypointChooser;
    public List<GameObject> navPointObjects;

    public void OnEnable()
    {
        navWaypointChooser = transform.GetComponent<NavWaypointChooser>();
        if (navWaypointChooser != null)
        {
            navWaypointChooser.Place += NavPointPlace;
            print("navWaypointChooser.Place += NavPointPlace");
        }
        NavWaypointMover.MoveComplete += ClearList;
    }

    void OnDisable()
    {
        if (navWaypointChooser != null)
        {
            navWaypointChooser.Place -= NavPointPlace;
        }
        print("navWaypointChooser.Place -= NavPointPlace");
        NavWaypointMover.MoveComplete -= ClearList;
    }

	
    public void NavPointPlace(Vector3 point)
    {

        GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
        NavWaypointRenderer navRender = o.GetComponent<NavWaypointRenderer>();

        if (navPointObjects.Count == 0)
        {
            navRender.target = transform.parent;
        }
        if (navPointObjects.Count > 0)
        {
            navRender.target = navPointObjects[(navPointObjects.Count - 1)].transform;          // tell object to point to last waypoint
        }
        navPointObjects.Add(o);
        return;
    } 

        void ClearList()
    {
        navPointObjects.Clear();
    }
}
