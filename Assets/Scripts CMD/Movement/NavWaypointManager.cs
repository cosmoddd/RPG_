using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{

    CombatCommandMove combatCommandMove;

    public GameObject navPointPrefab;
    public GameObject lineRendererPrefab;

    public List<GameObject> navPointObjects;
    
    public float distanceSoFar = 0;
    public float maxDistance = 15;

    public float distanceTest;

    public GameObject lineRenderObject;

    LineRenderer lineRenderer;
    DrawPathway drawPathway;

    public GameObject navPointPrefabSpawned;

    void Start()
    {
        combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();

        combatCommandMove.Clicked += NavPointPlace;
        combatCommandMove.RightClicked += DeSpawn;

        NavWaypointMover.MoveComplete += ClearList;
        NavWaypoint.WayPointClicked += ReSpawnLineRenderer;

        lineRenderObject = SpawnLineRenderer(this.transform.parent.gameObject);
        SetupDependencies();
        drawPathway.SetAgentSource(this.transform.parent.parent.gameObject);   
    }

    void SetupDependencies()
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

    public void NavPointPlace(Vector3 point)  // places next nav point in the world space
    {

        DistanceUpdate(drawPathway.distance);
        navPointPrefabSpawned = Object.Instantiate(navPointPrefab, new Vector3          //instantiate new navpoint
                                                    (point.x, (point.y + 1), point.z), 
                                                    Quaternion.identity);
        navPointObjects.Add(navPointPrefabSpawned);                                     //add new navpoint to the list
        lineRenderObject.transform.SetParent(navPointPrefabSpawned.transform);          //set line render object to previous navpoint
        drawPathway.enabled = false;                                                    //disable draw pathway script
 
        lineRenderObject = SpawnLineRenderer(navPointPrefabSpawned);                    //spawn new line renderer (function)
        SetupDependencies();
        return;
    }

    public GameObject SpawnLineRenderer(GameObject target)  // activates line renderer script on Nav Point
    {
        GameObject l = Object.Instantiate(lineRendererPrefab, target.transform.position, Quaternion.identity);  //spawn new line renderer
        l.transform.SetParent(this.transform.parent);            // keeps the parent as the source
        l.GetComponent<DrawPathway>().SetAgentSource(target);   
        return l;
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
            print("yams");
            navPointPrefabSpawned.AddComponent<BoxCollider>();
            return;
        }

        if (navPointPrefabSpawned != null && (lineRenderer.enabled == false))
        {
            print("blarney stone");
            lineRenderer.enabled = false;
            ClearMostRecentPoint();
            return;
        }
        else{
                DestroyEverything();
                return;
        }
    }


    void DistanceUpdate(float d)                //updates cumulative distance of nav points
    {
        distanceSoFar += d;
    }

    void ClearMostRecentPoint()
    {

        this.gameObject.AddComponent<ClearMostRecentPoint>().Clear(navPointObjects, 
                                                                    lineRenderObject, 
                                                                    drawPathway, 
                                                                    this, 
                                                                    lineRenderer, combatCommandMove);
/*         ClearMostRecentPoint clear = this.gameObject.AddComponent<ClearMostRecentPoint>();
        clear.Clear(navPointObjects, lineRenderObject, drawPathway, navPointPrefabSpawned, lineRenderer, combatCommandMove); */
    }

    void ClearList()                                    // removes all nav points and resets the navpoint system after movement
    {
        Destroy(lineRenderObject);
        lineRenderObject = SpawnLineRenderer(this.transform.parent.parent.gameObject);
        navPointObjects.Clear();
        distanceSoFar = 0;
    }

    void DestroyEverything(){                           //destroyes entire movement command hierarchy and starts from scratch
        
        combatCommandMove.Clicked -= NavPointPlace;
        combatCommandMove.RightClicked -= DeSpawn;
        NavWaypointMover.MoveComplete -= ClearList;
        NavWaypoint.WayPointClicked -= combatCommandMove.Ready;
        NavWaypoint.WayPointClicked -= ReSpawnLineRenderer;
            Destroy(lineRenderObject);
            Destroy(this.gameObject); 
    }

}
