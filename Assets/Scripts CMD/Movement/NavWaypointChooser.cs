using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWaypointChooser : MonoBehaviour
{

    public delegate void GetDistance(float d, Vector3 p);
    public event GetDistance DistanceSet;

    public delegate void VectorPick(Vector3 v);
    public event VectorPick Place;

    public List<Vector3> navPoints;

    CombatCommandMove combatCommandMove;

    float pointDistance;

    Vector3 point;

    public void OnEnable()
    {
        combatCommandMove = transform.GetComponent<CombatCommandMove>();
        if (combatCommandMove != null)
        {
            combatCommandMove.Clicked += SetNav;
            print("CombatCommandMove.Clicked subscribed");
        }
    }

    void OnDisable()
    {
        if (combatCommandMove != null)
        {
            combatCommandMove.Clicked -= SetNav;
        }
        print("CombatCommandMove.Clicked unsub");
    }


    public void SetNav()

    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                point = interactionInfo.point;
                navPoints.Add(point);
                Place(interactionInfo.point);
                return;
            }
    }


}

