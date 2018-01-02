using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour {

	public GameObject navPointPrefab;
    public List<GameObject> navPointObjects;
    public float distance = 0;
    public float maxDistance = 15;

    void Start ()
    {
        CombatCommandMove combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();
        NavWaypointSpawner.ReportDistance += DistanceUpdate;
        combatCommandMove.Clicked += NavPointPlace;
        NavWaypointMover.MoveComplete += ClearList;
    }
	
    public void NavPointPlace(Vector3 point)
    {
                {


                    if (navPointObjects.Count == 0)
                    {
                        if (GetTheDistance.Calculate((GetThePath.CalculatePath(this.transform.parent.gameObject, point))) < maxDistance)
                        {
                            GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
                            NavWaypointSpawner navRender = o.GetComponent<NavWaypointSpawner>();
                            navRender.target = transform.parent;
                            navPointObjects.Add(o);
                        }
                        else
                        {
                            print("too far");
                        }
                    }
                    if (navPointObjects.Count > 0)
                    {
                         if (GetTheDistance.Calculate((GetThePath.CalculatePath(navPointObjects[(navPointObjects.Count - 1)], point))) < maxDistance)
                        {
                            GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
                            NavWaypointSpawner navRender = o.GetComponent<NavWaypointSpawner>();
                            navRender.target = transform.parent;
                            navPointObjects.Add(o);
                           navRender.target = navPointObjects[(navPointObjects.Count - 1)].transform;          // tell object to point to last waypoint

                        }
                        else
                        {
                            print("too far");
                        }
                    }

                    return;
                }
    } 

        void DistanceUpdate(float d)
        {
            distance += d;    
        }

        void ClearList()
    {
        navPointObjects.Clear();
        distance = 0;
    }
}
