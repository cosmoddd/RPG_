using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour {

	public GameObject navPointPrefab;
    public GameObject lineRendererPrefab;
    public List<GameObject> navPointObjects;
    public float distance = 0;
    public float maxDistance = 15;

    GameObject lineRender;

    void Start ()
    {
        CombatCommandMove combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();
        combatCommandMove.Clicked += NavPointPlace;
        CombatCommandMove.Move += DisableDrawPathway;
        NavWaypointMover.MoveComplete += ClearList;

        lineRender = SpawnLineRenderer(this.transform.parent.gameObject);
        lineRender.GetComponent<DrawPathway>().GetAgentSource(this.transform.parent.gameObject);

    }
	
    public void NavPointPlace(Vector3 point)
    {      
        GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
        lineRender.transform.SetParent(o.transform);
        navPointObjects.Add(o);
        lineRender = SpawnLineRenderer(o);
        return;
    }

    public GameObject SpawnLineRenderer(GameObject target)
    {
        GameObject l = Object.Instantiate(lineRendererPrefab, target.transform.position, Quaternion.identity);
        l.transform.SetParent(this.transform.parent);  // still keeps the parent as the source
        l.GetComponent<DrawPathway>().GetAgentSource(target);
        return l;
    }

    void DisableDrawPathway()
    {
        GameObject l = transform.parent.GetComponentInChildren<LineRenderer>().gameObject;
        l.SetActive(false);
    }

    void DistanceUpdate(float d)
    {
        distance += d;
    }

    void ClearList()
    {
        navPointObjects.Clear();
        distance = 0;
    }

}
