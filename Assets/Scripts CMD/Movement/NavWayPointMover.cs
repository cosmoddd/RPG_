using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointMover : MonoBehaviour {

    static NavWaypointMover navWaypointMover;

    public delegate void NavWayDelegate ();
    public static event NavWayDelegate MoveComplete;

    List<GameObject> navPointz;
    public bool isPaused = false;

    CombatCommandMove combatCommandMove;
    NavWaypointManager navWaypoints;
    
    public NavMeshAgent navMeshAgent;

    public void OnEnable()
    {
        combatCommandMove = GetComponent<CombatCommandMove>();
        navWaypoints = GetComponent<NavWaypointManager>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        CombatCommandMove.Move += Initialize;
        navPointz = navWaypoints.navPointObjects;
        StartCoroutine ("MoveToWaypoint");
        print("Mover init");
    }

    public void Initialize()
    {
        StartCoroutine ("MoveToWaypoint");
    }

    IEnumerator MoveToWaypoint()
    {

        for (int i = 0; i < navPointz.Count; i++)
        {
            Vector3 destination = new Vector3(navPointz[i].transform.position.x,
                                                gameObject.transform.parent.position.y,
                                                navPointz[i].transform.position.z);
            print("go");
            while ((gameObject.transform.parent.position != destination))
            {
                navMeshAgent.destination = destination;
                yield return null;
            }
            yield return null;
        }
        print("done moving");
        MoveComplete();
        yield return null;

        Destroy(this);

    }

    void OnDisable()
    {
        CombatCommandMove.Move -= Initialize;
    }


}
