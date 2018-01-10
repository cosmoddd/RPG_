using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypoints : MonoBehaviour
{
    public delegate void VectorPick(Vector3 v);
    public event VectorPick Place;
    public List<Vector3> navPoints;
    CommandMove combatCommandMove;
    float pointDistance;
    Vector3 point;

#region Events
    public void OnEnable()
    {
        combatCommandMove = transform.GetComponent<CommandMove>();
        if (combatCommandMove != null)
        {
            combatCommandMove.Clicked += SetNav;
            print("CombatCommandMove.Clicked subscribed");
        }
        NavWaypointMover.MoveComplete += ClearNavPoints;
    }

    void OnDisable()
    {
        if (combatCommandMove != null)
        {
            combatCommandMove.Clicked -= SetNav;
        }
        print("CombatCommandMove.Clicked unsub");
        NavWaypointMover.MoveComplete -= ClearNavPoints;
    }

#endregion

    public void SetNav(Vector3 p)
    {
                navPoints.Add(p);
                Place(p);
                return;
            }


    public void ClearNavPoints()
    {
        navPoints.Clear();
    }
}

