using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalc : MonoBehaviour {

	CommandMove commandMove;
    public PathwayLength pathwayLength;

    public float cumulativeDistance = 0;
    public float currentDistance = 0;
    public float maxDistance;

    public List <float> currentDistanceSaved;


	void OnEnable(){

  		commandMove = this.transform.GetComponent<CommandMove>(); 
        commandMove.Clicked += DistanceAdd;

	}

    public void Update()
    {

        maxDistance = commandMove.combatController.listLengthAvailable;   // DISTANCE PLACABLE BY AVAILABLE COMMAND POINTS

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
        //
        // extension method  --V?
        //

    void DistanceAdd(Vector3 v)                //updates cumulative distance of nav points  // NEEDS TO BE CHANGED
    {
        currentDistanceSaved.Add(currentDistance);


        for (int i = Mathf.RoundToInt(cumulativeDistance); 
                    i < Mathf.RoundToInt((currentDistance + cumulativeDistance)); 
                    i++)
                    {
                        commandMove.combatController.CommandQueue.Insert(i, commandMove);
                    }

        cumulativeDistance = currentDistance + cumulativeDistance;
        currentDistance = 0f;
    }
    
    void Subtract()
    {
         for (int i = Mathf.RoundToInt(cumulativeDistance); 
                    i >= Mathf.RoundToInt(cumulativeDistance-currentDistanceSaved[currentDistanceSaved.Count -1]); 
                    i--)
                    {
                        commandMove.combatController.CommandQueue.RemoveAt(i);
                    }
        cumulativeDistance = cumulativeDistance - currentDistanceSaved[currentDistanceSaved.Count -1];
        currentDistanceSaved.RemoveAt(currentDistanceSaved.Count -1);
    }
        //
        // extension method?  --^?
        //

    public void ResetDistance()
    {
        cumulativeDistance = 0f;
    }

    void OnDisable()
    {
        commandMove.Clicked -= DistanceAdd; 
    }
}
