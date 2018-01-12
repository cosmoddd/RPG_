﻿using System.Collections;
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
        commandMove.distanceCalc = this;

        NavWaypointMover.MoveComplete += ResetDistance;
	}

    public void Update()
    {
        if (pathwayLength == null)
        {                      
            pathwayLength = this.transform.parent.GetComponentInChildren<PathwayLength>();
            return;
        }

        currentDistance = pathwayLength.distance;
    }

    void DistanceAdd(Vector3 v)                //updates cumulative distance of nav points
    {
        print("Distance Calc clicked");
        currentDistanceSaved.Add(currentDistance);
        cumulativeDistance = currentDistance + cumulativeDistance;
        pathwayLength = this.transform.parent.GetComponentInChildren<PathwayLength>();
    }
    
    void Subtract()
    {
        print("subtracted");
        cumulativeDistance = cumulativeDistance - currentDistanceSaved[currentDistanceSaved.Count -1];
        currentDistanceSaved.RemoveAt(currentDistanceSaved.Count -1);
        pathwayLength = this.transform.parent.GetComponentInChildren<PathwayLength>();
    }

    void ResetDistance()
    {
        cumulativeDistance = 0f;
    }

    void OnDisable()
    {
        NavWaypointMover.MoveComplete -= ResetDistance;  
        commandMove.Clicked -= DistanceAdd; 
    }
}
