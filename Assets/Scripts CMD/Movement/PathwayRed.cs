﻿using System.Collections;
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
            print("getting the distance compare item");
			print(this.transform.parent);
            distanceCompare = this.transform.parent.GetComponentInChildren<DistanceCompare>();
            distanceCompare.InsideRange += InsideRange;
            distanceCompare.OutOfRange += OutOfRange;
			return;
        }

        if (commandMove == null){

            print("getting the command move item");
			print(this.transform.parent);
            commandMove = this.transform.parent.GetComponentInChildren<CommandMove>();
            commandMove.RightClicked += InsideRangeRC;
			return;
        }

    }

    void Subscribe()
    {

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
