using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour {

	public GameObject navPointPrefab;
    public GameObject lineRendererPrefab;
    public List<GameObject> navPointObjects;
    public float distanceSoFar = 0;
    public float maxDistance = 15;

    public float distanceTest;

    GameObject lineRender;

    CombatCommandMove combatCommandMove;

    void Start ()
    {
        combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();
    

        combatCommandMove.Clicked += NavPointPlace;
        NavWaypointMover.MoveComplete += ClearList;
        combatCommandMove.RightClicked += DeSpawnLineRenderer;

        lineRender = SpawnLineRenderer(this.transform.parent.gameObject);
        lineRender.GetComponent<DrawPathway>().SetAgentSource(this.transform.parent.parent.gameObject);

    }
	
    public void NavPointPlace(Vector3 point)
    {      
        DistanceUpdate(lineRender.GetComponent<DrawPathway>().distance);
        GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
        lineRender.transform.SetParent(o.transform);
        lineRender.GetComponent<DrawPathway>().enabled = false;
        navPointObjects.Add(o);
        lineRender = SpawnLineRenderer(o);
        return;
    }

    public GameObject SpawnLineRenderer(GameObject target)
    {
        GameObject l = Object.Instantiate(lineRendererPrefab, target.transform.position, Quaternion.identity);
        l.transform.SetParent(this.transform.parent);  // still keeps the parent as the source
        l.GetComponent<DrawPathway>().SetAgentSource(target);
        return l;
    }

    public void DeSpawnLineRenderer(Vector3 v)

    {
        lineRender.GetComponent<LineRenderer>().enabled = false;
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

    void ClearList()
    {
        Destroy(lineRender);
        lineRender = SpawnLineRenderer(this.transform.parent.parent.gameObject);
        navPointObjects.Clear();
        distanceSoFar = 0;
    }

}
