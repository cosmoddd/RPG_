using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointSpawner : MonoBehaviour {  //SPAWNS WAYPOINTS

LineRenderer line; //to hold the line Renderer
public LineRenderer hostLine;

public Vector3[] pathway;
public float lineOffset = 1;

public void Start(){

/*     CombatCommandMove.Move += DisableNavMesh; */
    NavWaypointMover.MoveComplete += DestroySelf;
    line = GetComponent<LineRenderer>(); //get the line renderer
    print("hungry!");

}

void Update(){
        for(int i = 0; i < pathway.Length; i++){
        print("blahblah");
		line.SetPosition(i, hostLine.GetPosition(i));
     }
}
/* IEnumerator DrawPath()
{
    if (target != null)
    {
        agent.SetDestination(target.position); //create the path pointing to target
        yield return null;                      // this yield is important.  do it!
    }
    pathway = agent.path.corners;
    ReportDistance(GetTheDistance.Calculate(agent.path));
      yield return null;
  
    line.positionCount = pathway.Length;

    for(int i = 0; i < pathway.Length; i++){
		line.SetPosition(i, new Vector3(pathway[i].x,((pathway[i].y)+(lineOffset)),pathway[i].z));
     }
    yield return null;
}
 */

/* void DisableNavMesh()
{
    CombatCommandMove.Move -= DisableNavMesh;

} */

void DestroySelf()
{
      NavWaypointMover.MoveComplete -= DestroySelf;
/*       CombatCommandMove.Move -= DisableNavMesh; */
      Destroy(gameObject);
}
}
