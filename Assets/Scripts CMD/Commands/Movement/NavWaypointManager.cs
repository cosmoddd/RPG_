using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWaypointManager : MonoBehaviour    // this class should specifically deal with the addition and deletion of waypoints
{
#region variables

    public CommandMove commandMove;
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
        commandMove = this.gameObject.GetComponent<CommandMove>();

        commandMove.Clicked += NavPointPlace;
        commandMove.RightClicked += DeSpawn;

        CombatController.DeSelectAllEvent += AddColliderToWaypoint;
        commandMove.combatController.SelectEvent += RemoveColliderFromWaypoint;

        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>().spawnLineRenderer(this, this.transform.parent.parent.gameObject, false);
        
        SetupDependencies();
    }

    public void SetupDependencies()
    {
        lineRenderer = lineRenderObject.GetComponent<LineRenderer>();
        pathwayDraw = lineRenderObject.GetComponent<PathwayDraw>();

        if (navPointPrefabSpawned != null)
            navPointPrefabSpawned.SendMessage ("SetupDependency", this.gameObject);
    }

    public void NavPointPlace(Vector3 point)  // places next nav point in the world space  // CORE USAGE OF THIS CLASS!
    {
        navPointPrefabSpawned = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);                                        
        navPointObjects.Add(navPointPrefabSpawned);                                     //add new navpoint to the list
        lineRenderObject.transform.SetParent(navPointPrefabSpawned.transform);          //set line render object to previous navpoint
        pathwayDraw.enabled = false;                                          //disable pathway draw script
        Destroy(lineRenderObject.GetComponent<PathwayActivate>());              //Destroy pathway activate script.

        lineRenderObject = this.gameObject.AddComponent<SpawnLineRenderer>().spawnLineRenderer(this, navPointPrefabSpawned, true);   //spawn new line renderer (function)
        SetupDependencies();
        return;
    }

    public void AddColliderToWaypoint()
    {
        this.gameObject.AddComponent<NavWaypointColliderControl>().AddColliderToWaypoint(navPointPrefabSpawned);
    }

    public void RemoveColliderFromWaypoint()
    {
        this.gameObject.AddComponent<NavWaypointColliderControl>().RemoveColliderFromWaypoint(navPointPrefabSpawned);
    }

    public void DeSpawn(Vector3 v)   
    {
        this.gameObject.AddComponent<NavRemove>().DeSpawn(navPointObjects, v, navPointPrefabSpawned, lineRenderObject, lineRenderer);
    }

    public void DeleteThis(){ // removes all nav points and resets the navpoint system after movement   MAY NO LONGER NEED THIS
    
        this.gameObject.AddComponent<NavDestroyEverything>().navDestroyEverything(this);  // destroy everything
        return;
    }

}
