using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour
{

    public GameObject navPointPrefab;
    public GameObject lineRendererPrefab;
    public List<GameObject> navPointObjects;
    public float distanceSoFar = 0;
    public float maxDistance = 15;

    public float distanceTest;

    GameObject lineRender;
    public GameObject navPointPrefabSpawned;

    CombatCommandMove combatCommandMove;

    void Start()
    {
        combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();

        combatCommandMove.Clicked += NavPointPlace;
        combatCommandMove.RightClicked += DeSpawn;

        NavWaypointMover.MoveComplete += ClearList;
        NavWaypoint.WayPointClicked += ReSpawnLineRenderer;

        lineRender = SpawnLineRenderer(this.transform.parent.gameObject);
        lineRender.GetComponent<DrawPathway>().SetAgentSource(this.transform.parent.parent.gameObject);

    }

    public void NavPointPlace(Vector3 point)
    {
        DistanceUpdate(lineRender.GetComponent<DrawPathway>().distance);
        navPointPrefabSpawned = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
        lineRender.transform.SetParent(navPointPrefabSpawned.transform);
        lineRender.GetComponent<DrawPathway>().enabled = false;
        navPointObjects.Add(navPointPrefabSpawned);
        lineRender = SpawnLineRenderer(navPointPrefabSpawned);
        return;
    }

    public GameObject SpawnLineRenderer(GameObject target)
    {
        GameObject l = Object.Instantiate(lineRendererPrefab, target.transform.position, Quaternion.identity);
        l.transform.SetParent(this.transform.parent);  // still keeps the parent as the source
        l.GetComponent<DrawPathway>().SetAgentSource(target);
        return l;
    }

    public void ReSpawnLineRenderer()

    {
        Destroy(navPointPrefabSpawned.GetComponent<BoxCollider>());
        lineRender.GetComponent<LineRenderer>().enabled = true;
    }


    public void DeSpawn(Vector3 v)

    {
        if (navPointPrefabSpawned != null && (lineRender.GetComponent<LineRenderer>().enabled == true))
        {
            lineRender.GetComponent<LineRenderer>().enabled = false;
            print("yams");
            navPointPrefabSpawned.AddComponent<BoxCollider>();
            return;
        }

        if (navPointPrefabSpawned != null && (lineRender.GetComponent<LineRenderer>().enabled == false))
        {
            lineRender.GetComponent<LineRenderer>().enabled = false;
            print("moo moo");
            ClearMostRecentPoint();
            return;
        }
        else{
                DestroyEverything();
                return;
        }
    }

    void Update()
    {
        distanceTest = lineRender.GetComponent<DrawPathway>().distance + distanceSoFar;
        if (lineRender.GetComponent<DrawPathway>().distance + distanceSoFar > maxDistance)
        {
            combatCommandMove.maxDistanceExceeded = true;
        }
        else
        {
            combatCommandMove.maxDistanceExceeded = false;
        }
    }


    void DistanceUpdate(float d)
    {
        distanceSoFar += d;
    }

    void ClearMostRecentPoint()
    {
        navPointObjects.Remove(navPointObjects[navPointObjects.Count-1]);
    }

    void ClearList()
    {
        Destroy(lineRender);
        lineRender = SpawnLineRenderer(this.transform.parent.parent.gameObject);
        navPointObjects.Clear();
        distanceSoFar = 0;
    }

    void DestroyEverything(){
        
        combatCommandMove.Clicked -= NavPointPlace;
        combatCommandMove.RightClicked -= DeSpawn;
        NavWaypointMover.MoveComplete -= ClearList;
        NavWaypoint.WayPointClicked -= combatCommandMove.Ready;
        NavWaypoint.WayPointClicked -= ReSpawnLineRenderer;
            Destroy(lineRender);
            Destroy(this.gameObject); 
    }

}
