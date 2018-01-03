using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWaypointManager : MonoBehaviour {

	public GameObject navPointPrefab;
    public List<GameObject> navPointObjects;
    public float distance = 0;
    public float maxDistance = 15;

    GameObject lineRender;

    void Start ()
    {
        CombatCommandMove combatCommandMove = this.gameObject.GetComponent<CombatCommandMove>();
        combatCommandMove.Clicked += NavPointPlace;
        NavWaypointMover.MoveComplete += ClearList;
    
        lineRender = this.transform.GetChild(0).gameObject;
    }
	
    public void NavPointPlace(Vector3 point)
    {      
        GameObject o = Object.Instantiate(navPointPrefab, new Vector3(point.x, (point.y + 1), point.z), Quaternion.identity);
        lineRender.transform.SetParent(o.transform);
        navPointObjects.Add(o);
        return;
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
