using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathwayActivate : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{
    public bool hoveringOverNav;
    CommandMove commandMove;
    CombatController c;
    public LineRenderer lineRenderer;

  public void OnEnable(){

        Selection.Enter += DeActivateNavLine;
        Selection.Exit += SelectionMouseExit;
    }


    public void Start(){

        commandMove = transform.parent.GetComponentInChildren<CommandMove>();
 c = commandMove.combatController;

        c.SelectEvent += Activate;

        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SelectionMouseExit()
    {
        if (commandMove.canPlaceWaypoint == true)
        {
            Activate();
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

    public void Activate()
    {

            lineRenderer.enabled = true;

    }

    public void DeActivateNavLine()
    {
           lineRenderer.enabled = false;
           return;
    }

    public void OnDisable(){

        Selection.Enter -= DeActivateNavLine;
        Selection.Exit -= SelectionMouseExit;
        c.SelectEvent -= Activate;
    }

}