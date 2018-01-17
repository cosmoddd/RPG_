using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathwayActivate : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{
    public bool hoveringOverNav;
    CommandMove commandMove;
    public LineRenderer lineRenderer;

  public void OnEnable(){

        Selection.Enter += DeActivateNavLine;
        Selection.Exit += SelectionMouseExit;

        NavWaypoint.WayPointHover += WayPointHover;
        NavWaypoint.WayPointHoverExit += WayPointHoverExit;
        NavWaypoint.WayPointClicked += ActivateNavLine;
    }

    public void Start(){

        commandMove = transform.parent.GetComponentInChildren<CommandMove>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void WayPointHover()
    {
        hoveringOverNav = true;
    }

    public void WayPointHoverExit()
    {
        hoveringOverNav = false;
    }

    public void SelectionMouseExit()
    {
        if (commandMove.canPlaceWaypoint == true)
        {
            lineRenderer.enabled = true;
            return;
        }
    }

    public void ActivateNavLine()       // respawns line renderer script once the most recent navpoint has been clicked.  dependant on navpoint
    {
        if (hoveringOverNav == true)
            {
                lineRenderer.enabled = true;
                return;
            }
        return;
    }

    public void DeActivateNavLine()
    {
           lineRenderer.enabled = false;
           return;
    }

    public void OnDisable(){

        Selection.Enter -= DeActivateNavLine;
        Selection.Exit -= SelectionMouseExit;

        NavWaypoint.WayPointHover -= WayPointHover;
        NavWaypoint.WayPointHoverExit -= WayPointHoverExit;
        NavWaypoint.WayPointClicked -= ActivateNavLine;
    }

}