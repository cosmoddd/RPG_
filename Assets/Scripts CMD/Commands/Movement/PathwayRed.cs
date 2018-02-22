using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathwayLength))]
[RequireComponent(typeof(LineRenderer))]


public class PathwayRed : MonoBehaviour
{

    DistanceCompare distanceCompare;
    CommandMove commandMove;
	LineRenderer lineRenderer;


    void Start()
    {
		lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (distanceCompare == null)
        {

            distanceCompare = this.transform.parent.GetComponentInChildren<DistanceCompare>();
            distanceCompare.InsideRange += InsideRange;
            distanceCompare.OutOfRange += OutOfRange;
			return;
        }

        if (commandMove == null){

            commandMove = this.transform.parent.GetComponentInChildren<CommandMove>();
            commandMove.RemoveWaypoint += InsideRangeRC;
			return;
        }

    }

    void InsideRange()	
	{
        lineRenderer.startColor = Color.blue;
		lineRenderer.endColor = Color.blue;
    }

    void InsideRangeRC(Vector3 v)	
	{
        if (lineRenderer != null)
        {
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        }
    }

    void OutOfRange()
    {
        lineRenderer.startColor = Color.red;
		lineRenderer.endColor = Color.red;
    }

    void OnDisable()
    {
        distanceCompare.InsideRange -= InsideRange;
        distanceCompare.OutOfRange -= OutOfRange;
    }

}
