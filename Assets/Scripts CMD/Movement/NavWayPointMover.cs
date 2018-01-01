using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointMover : MonoBehaviour {

    public delegate void NavWayDelegate ();
    public static event NavWayDelegate MoveComplete;


    public List<Vector3> navPointz;
    public bool isPaused = false;

    CombatCommandMove combatCommandMove;
    NavWaypointChooser navWaypointChooser;
    
    public NavMeshAgent daAgent;

    public void OnEnable()
    {
        combatCommandMove = GetComponent<CombatCommandMove>();
        navWaypointChooser = GetComponent<NavWaypointChooser>();
        daAgent = GetComponentInParent<NavMeshAgent>();
        CombatCommandMove.Move += Initialize;
        navPointz = navWaypointChooser.navPoints;
        print("Mover init");
    }

    void Initialize()
    {
        StartCoroutine ("MoveToWaypoint");
    }

    IEnumerator MoveToWaypoint()
    {

        for (int i = 0; i < navPointz.Count; i++)
        {
            Vector3 destination = new Vector3(navPointz[i].x,gameObject.transform.parent.position.y,navPointz[i].z);
            print("go");
            while ((gameObject.transform.parent.position != destination))
            {
                daAgent.destination = destination;
                yield return null;
            }
            yield return null;
        }
        print("done moving");
        MoveComplete();
        yield return null;

        Destroy(this);

    }
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
                isPaused = !isPaused;        
        }
    }

    void OnDisable()
    {
        CombatCommandMove.Move -= Initialize;
    }


}
