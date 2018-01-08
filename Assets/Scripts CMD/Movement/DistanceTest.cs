using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTest : MonoBehaviour {

	public CombatCommandMove combatCommandMove;
    public DrawPathway drawPathway;

    public float distanceSoFar = 0;
    public float maxDistance = 15;
    public float distanceTestFloat = 0;

	void Start(){
	
        NavWaypointMover.MoveComplete += ResetDistance;
        
		combatCommandMove = this.transform.GetComponent<CombatCommandMove>();
	}

    public void Update()
            {
                if (drawPathway == null)
                {                      
                    drawPathway = this.transform.parent.GetComponentInChildren<DrawPathway>();
                    return;
                }
                print("gummy");
                distanceTestFloat = drawPathway.distance + distanceSoFar;
                }

    void DistanceUpdate(float d)                //updates cumulative distance of nav points
    {
        distanceSoFar += d;
    }

    void ResetDistance()
    {
        distanceSoFar = 0f;
    }

    void OnDisable()
    {
        print("distance test disable");
     	
        NavWaypointMover.MoveComplete -= ResetDistance;   
    }
}
