﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{
#region variables

    public CombatCommandMove combatCommandMove;

    public GameObject navPointPrefab;
    public GameObject lineRendererPrefab;

    public List<GameObject> navPointObjects;
    
    public float distanceSoFar = 0;
    public float maxDistance = 15;

    public float distanceTest;

    public GameObject lineRenderObject;

    LineRenderer lineRenderer;
    public DrawPathway drawPathway;

    public GameObject navPointPrefabSpawned;
#endregion

    void Start()
    {
        combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();

        combatCommandMove.Clicked += NavPointPlace;
        combatCommandMove.RightClicked += DeSpawn;

        NavWaypointMover.MoveComplete += ClearList;
        NavWaypoint.WayPointClicked += ReSpawnLineRenderer;

        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>()._SpawnLineRenderer(this, this.transform.parent.gameObject);
        SetupDependencies();
        drawPathway.SetAgentSource(this.transform.parent.parent.gameObject);   
    }

    public void SetupDependencies()
    {
        lineRenderer = lineRenderObject.GetComponent<LineRenderer>();
        drawPathway = lineRenderObject.GetComponent<DrawPathway>();
    }

        void Update()  //checks if distance on line renderer has been exceeded or not
        {
            distanceTest = drawPathway.distance + distanceSoFar;
            if (drawPathway.distance + distanceSoFar > maxDistance)
            {
                combatCommandMove.maxDistanceExceeded = true;
                return;
            }
            else
            {
                combatCommandMove.maxDistanceExceeded = false;
                return;
            }
        }

    public void NavPointPlace(Vector3 point)  // places next nav point in the world space  // CORE USAGE OF THIS CLASS!
    {
        DistanceUpdate(drawPathway.distance);
        navPointPrefabSpawned = Object.Instantiate(navPointPrefab, new Vector3          //instantiate new navpoint
                                                    (point.x, (point.y + 1), point.z), 
                                                    Quaternion.identity);
        navPointObjects.Add(navPointPrefabSpawned);                                     //add new navpoint to the list
        lineRenderObject.transform.SetParent(navPointPrefabSpawned.transform);          //set line render object to previous navpoint
        drawPathway.enabled = false;                                                    //disable draw pathway script
 
        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>()._SpawnLineRenderer(this, navPointPrefabSpawned);                    //spawn new line renderer (function)
        SetupDependencies();
        return;
    }

    public void ReSpawnLineRenderer()       // respawns line renderer script once the most recent navpoint has been clicked.  dependant on navpoint

    {
        Destroy(navPointPrefabSpawned.GetComponent<BoxCollider>());
        lineRenderer.enabled = true;
    }

    public void DeSpawn(Vector3 v)      //checker for dynamically retracting the placed navpoints

    {
        if (navPointPrefabSpawned != null && (lineRenderer.enabled == true))
        {
            lineRenderer.enabled = false;
            navPointPrefabSpawned.AddComponent<BoxCollider>();
            return;
        }

        if (navPointPrefabSpawned != null && (lineRenderer.enabled == false))
        {
            lineRenderer.enabled = false;
            ClearMostRecentPoint();
            return;
        }
        else{
                NavWaypointMover.MoveComplete -= ClearList;
                NavWaypoint.WayPointClicked -= combatCommandMove.Ready;
                NavWaypoint.WayPointClicked -= ReSpawnLineRenderer;
                this.gameObject.AddComponent<NavDestroyEverything>()._NavDestroyEverything(this);  // destroy everything
                return;
        }
    }


    void DistanceUpdate(float d)                //updates cumulative distance of nav points
    {
        distanceSoFar += d;
    }

    void ClearMostRecentPoint()
    {
        this.gameObject.AddComponent<ClearMostRecentPoint>().Clear(navPointObjects,lineRenderObject,drawPathway, this,  lineRenderer, combatCommandMove);
    }

    public void ClearList()                                    // removes all nav points and resets the navpoint system after movement
    {
        Destroy(lineRenderObject);
        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>()._SpawnLineRenderer(this, this.transform.parent.parent.gameObject);
        navPointObjects.Clear();
        distanceSoFar = 0;
    }

}
