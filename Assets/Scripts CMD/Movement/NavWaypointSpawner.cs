using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointSpawner : MonoBehaviour {  //SPAWNS WAYPOINTS

LineRenderer line; //to hold the line Renderer
public LineRenderer hostLine;
public Vector3[] hostPathway;
int positions;
public float lineOffset = 1;

public void Start(){

    NavWaypointMover.MoveComplete += DestroySelf;
}


void DestroySelf()
{
      NavWaypointMover.MoveComplete -= DestroySelf;
      Destroy(gameObject);
}
}
