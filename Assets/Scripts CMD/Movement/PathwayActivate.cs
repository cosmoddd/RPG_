using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathwayActivate : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{

    public LineRenderer lineRenderer;

    public void Start(){

        lineRenderer = GetComponent<LineRenderer>();
    }

    public void OnEnable(){

        Selection.MouseOver += DeActivateNavLine;
        Selection.MouseExit += ActivateNavLine;
    }

    public void ActivateNavLine()       // respawns line renderer script once the most recent navpoint has been clicked.  dependant on navpoint
    {
            print("Activate line");
           lineRenderer.enabled = true;
           return;
    }

    public void DeActivateNavLine()
    {
           lineRenderer.enabled = false;
           return;
    }

    public void OnDisable(){

        Selection.MouseOver -= DeActivateNavLine;
        Selection.MouseExit -= ActivateNavLine;
        
    }

}