using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalc : MonoBehaviour {

	CommandMove commandMove;
    public PathwayLength pathwayLength;

    public float cumulativeDistance = 0;
    public float currentDistance = 0;
    public float maxDistance =20;

    public List <float> currentDistanceSaved;


	void OnEnable(){

  		commandMove = this.transform.GetComponent<CommandMove>(); 
        commandMove.Clicked += DistanceAdd;
	}

    public void Update()
    {
        if (commandMove.canPlaceWaypoint == true)
           {
            CalculateDistance();
           } 
        else{
                return;
            }

        if (pathwayLength != null)
        {
          currentDistance = pathwayLength.distance;
        }
    }

    void CalculateDistance()
    {      
        if (pathwayLength == null || pathwayLength != this.transform.parent.GetComponentInChildren<PathwayLength>())
        {                      
            pathwayLength = this.transform.parent.GetComponentInChildren<PathwayLength>();
        }
    }

    void DistanceAdd(Vector3 v)                //updates cumulative distance of nav points
    {
        currentDistanceSaved.Add(currentDistance);
        cumulativeDistance = currentDistance + cumulativeDistance;
        currentDistance = 0f;
    }
    
    void Subtract()
    {
        cumulativeDistance = cumulativeDistance - currentDistanceSaved[currentDistanceSaved.Count -1];
        currentDistanceSaved.RemoveAt(currentDistanceSaved.Count -1);
    }

    public void ResetDistance()
    {
        cumulativeDistance = 0f;
    }

    void OnDisable()
    {
        commandMove.Clicked -= DistanceAdd; 
    }
}
