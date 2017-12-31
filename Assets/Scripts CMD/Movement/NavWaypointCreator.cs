using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWaypointCreator : MonoBehaviour
{

    public delegate void GetDistance(float d, Vector3 p);
    public event GetDistance DistanceSet;

    public GameObject navPointPrefab;
    public List<GameObject> navPointObjects;
    public List<Vector3> navPoints;

    CombatCommandMove combatCommandMove;

    float pointDistance;
    public Vector3 point1, point2;
    bool startingPoint;

    public void OnEnable()
    {

        combatCommandMove = transform.GetComponent<CombatCommandMove>();
        if (combatCommandMove != null)
        {
            combatCommandMove.Clicked += SetNav;
            print("CombatCommandMove.Clicked subscribed");
        }
        NavWaypointMover.MoveComplete += NavPointsDestroy;

        startingPoint = true;

    }

    void OnDisable()
    {

        startingPoint = false;

        if (combatCommandMove != null)
        {
            combatCommandMove.Clicked -= SetNav;
        }

        NavWaypointMover.MoveComplete -= NavPointsDestroy;
        print("CombatCommandMove.Clicked unsub");
    }


    public void SetNav()

    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            if (startingPoint == true)
            {
                point1 = interactionInfo.point;
                navPoints.Add(point1);
                startingPoint = false;
                NavPointPlace(interactionInfo.point);
                return;
            }
        if (startingPoint == false)
        {
            point2 = interactionInfo.point;
            navPoints.Add(point2);
            pointDistance = Vector3.Distance(point1, point2);
            //		DistanceSet(pointDistance, point2);  // event for calling distance and setting it
            point1 = point2;
            NavPointPlace(interactionInfo.point);
            return;
        }

    }


    public void NavPointPlace(Vector3 point)
    {

        GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
        NavWaypointRenderer navRender = o.GetComponent<NavWaypointRenderer>();

        if (navPointObjects.Count != 0)
        {
            navRender.target = navPointObjects[(navPointObjects.Count - 1)].transform;          // tell object to point to last waypoint
        }
        navPointObjects.Add(o);
        return;
    }

    void NavPointsDestroy()
    {
        for (int i = 0; i < navPointObjects.Count; i++)
        {
            GameObject.Destroy(navPointObjects[i]);
        }
        navPointObjects.Clear();
        navPoints.Clear();
    }




}

