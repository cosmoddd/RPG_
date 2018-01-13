using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWaypointManager : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{
#region variables

    public CommandMove combatCommandMove;
    public GameObject navPointPrefab;
    public GameObject lineRendererPrefab;
    public List<GameObject> navPointObjects;
    public GameObject lineRenderObject;
    LineRenderer lineRenderer;
    PathwayLength pathwayLength;
    PathwayDraw pathwayDraw;
    public GameObject navPointPrefabSpawned;

#endregion

    void Start()
    {
        combatCommandMove = this.gameObject.GetComponent<CommandMove>();

        combatCommandMove.Clicked += NavPointPlace;

        combatCommandMove.RightClicked += DeSpawn;
        NavWaypointMover.MoveComplete += DeleteThis;
    //    NavWaypoint.WayPointClicked += ReActivateLineRenderer;
    
        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>()._SpawnLineRenderer(this, this.transform.parent.parent.gameObject);
        SetupDependencies();
    }

    public void SetupDependencies()
    {
        lineRenderer = lineRenderObject.GetComponent<LineRenderer>();
        pathwayDraw = lineRenderObject.GetComponent<PathwayDraw>();
    }

    public void NavPointPlace(Vector3 point)  // places next nav point in the world space  // CORE USAGE OF THIS CLASS!
    {
        navPointPrefabSpawned = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);                                        
        navPointObjects.Add(navPointPrefabSpawned);                                     //add new navpoint to the list
        lineRenderObject.transform.SetParent(navPointPrefabSpawned.transform);          //set line render object to previous navpoint
        pathwayDraw.enabled = false;                                          //disable pathway draw script
        Destroy(lineRenderObject.GetComponent<PathwayActivate>());              //Destroy pathway activate script.

        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>()._SpawnLineRenderer(this, navPointPrefabSpawned);   //spawn new line renderer (function)
        SetupDependencies();
        return;
    }
    
    //control when this ----v gets spawned

    /* 
    
    public void ReActivateLineRenderer()       // respawns line renderer script once the most recent navpoint has been clicked.  dependant on navpoint
    {
           lineRenderer.enabled = true;
           Destroy(navPointPrefabSpawned.GetComponent<BoxCollider>());
           return;
    }

    public void DeActivateLineRenderer()
    {

    }
    */ 

    public void DeSpawn(Vector3 v)   
    {
        this.gameObject.AddComponent<NavRemove>().DeSpawn(navPointObjects, v, navPointPrefabSpawned, lineRenderObject, lineRenderer);
    }

    void DeleteThis(){ // removes all nav points and resets the navpoint system after movement
        NavWaypointMover.MoveComplete -= DeleteThis;
        NavWaypoint.WayPointClicked -= combatCommandMove.Ready;
//        NavWaypoint.WayPointClicked -= ReActivateLineRenderer;
        this.gameObject.AddComponent<NavDestroyEverything>()._NavDestroyEverything(this);  // destroy everything
        return;
    }

}
