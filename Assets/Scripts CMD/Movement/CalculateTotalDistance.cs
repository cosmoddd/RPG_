using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateTotalDistance : MonoBehaviour {

	public CommandMove combatCommandMove;
    public PathwayDraw drawPathway;

    public float cumulativeDistance = 0;
    public float currentDistance = 0;
    public float maxDistance =20;

    public List <float> currentDistanceSaved;

	void Start(){

  		combatCommandMove = this.transform.GetComponent<CommandMove>();    
        NavWaypointMover.MoveComplete += ResetDistance;
        combatCommandMove.Clicked += DistanceAdd;
	}

    public void Update()
            {
                if (drawPathway == null)
                {                      
                    drawPathway = this.transform.parent.GetComponentInChildren<PathwayDraw>();
                    return;
                }
                
                currentDistance = drawPathway.distance;

// move this to the CommandMove master function

                if (currentDistance + cumulativeDistance > maxDistance)
                {
                    combatCommandMove.ready = false;
                }
                else {
                    combatCommandMove.ready = true;
                }

// move this to the CommandMove master function


            }

    void DistanceAdd(Vector3 v)                //updates cumulative distance of nav points
    {
        currentDistanceSaved.Add(currentDistance);
        cumulativeDistance = currentDistance + cumulativeDistance;
    }

    void Subtract()
    {
        cumulativeDistance = cumulativeDistance - currentDistanceSaved[currentDistanceSaved.Count -1];
        currentDistanceSaved.RemoveAt(currentDistanceSaved.Count -1);
    }

    void ResetDistance()
    {
        cumulativeDistance = 0f;
    }

    void OnDisable()
    {
        print("distance test disable");
    
        NavWaypointMover.MoveComplete -= ResetDistance;   
    }


}
