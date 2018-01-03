using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawPathway : MonoBehaviour {  //DRAWS WAYPOINTS

	public NavMeshAgent navMeshAgent;
	NavMeshPath path;
	LineRenderer line;
    CombatCommandMove host;
	public Vector3[] pathway;
    public float lineOffset = 1;
	public float totalDistance;

	void Start () {

	    host = this.gameObject.transform.parent.GetComponentInChildren<CombatCommandMove>();
        host.Clicked += DeactivateScript;
   //     CombatCommandMove.Move += Disable;

	    line = GetComponent<LineRenderer>(); //get the line renderer
		navMeshAgent.isStopped = true;
	}
	
    public void SetAgentSource (GameObject o)
    {
        navMeshAgent = o.GetComponent<NavMeshAgent>();
    }

	void Update(){

		Vector3 targetPoint = CastRay();
        if (targetPoint != Vector3.zero)
        {
            navMeshAgent.SetDestination(targetPoint); //create the path pointing to target
		}
        pathway = navMeshAgent.path.corners;
        line.positionCount = pathway.Length;

        for (int i = 0; i < pathway.Length; i++)
        {
            line.SetPosition(i, new Vector3(pathway[i].x, ((pathway[i].y) + (lineOffset)), pathway[i].z));
        }

    }

	public Vector3 CastRay()

    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                return interactionInfo.point;
            }
		else return Vector3.zero;
    }



    public void DeactivateScript(Vector3 point){

        print("Draw Pathway Deactivated");
        host.Clicked -= DeactivateScript;
        this.enabled = false;
        return;

    }

}

