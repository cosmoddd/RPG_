using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalc : MonoBehaviour {

	CommandMove commandMove;
    public PathwayLength pathwayLength;

    public float cumulativeDistance = 0;
    public float currentDistance = 0;
    public float maxDistance =20;

    public bool ready; 

    public List <float> currentDistanceSaved;

    void Start(){

        ready = true;
    }

	void OnEnable(){

  		commandMove = this.transform.GetComponent<CommandMove>(); 

        commandMove.Clicked += DistanceAdd;

        Selection.Enter += Unready;
        Selection.Exit += HoverExitReady;

        NavWaypoint.WayPointClicked += NavClickedReady;
        NavWaypointMover.MoveComplete += ResetDistance;
	}

    public void Update()
    {
        if (pathwayLength == null || pathwayLength != this.transform.parent.GetComponentInChildren<PathwayLength>())
        {                      
            pathwayLength = this.transform.parent.GetComponentInChildren<PathwayLength>();
            return;
        }

        while (ready == false)
        {
            return;
        }

        currentDistance = pathwayLength.distance;
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

    void ResetDistance()
    {
        cumulativeDistance = 0f;
    }

    void NavClickedReady()
    {
             ready = true;
    }

    void HoverExitReady ()
    {
        if (commandMove.canPlaceWaypoint == true)
        {
             ready = true;
        }
    }

    void Unready ()
    {
        ready = false;
    }

    void OnDisable()
    {
        NavWaypointMover.MoveComplete -= ResetDistance;  
        NavWaypoint.WayPointClicked -= NavClickedReady;

        commandMove.Clicked -= DistanceAdd; 
        
        Selection.Enter -= Unready;
        Selection.Exit -= HoverExitReady;

    }
}
