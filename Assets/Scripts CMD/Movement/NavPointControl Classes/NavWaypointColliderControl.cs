using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWaypointColliderControl : MonoBehaviour {


    public void AddColliderToWaypoint(GameObject o)
    {
        if (o != null && o.GetComponent<BoxCollider>() == null)
        {
            o.AddComponent<BoxCollider>();
        }
		Destroy(this);
    }

    
    public void RemoveColliderFromWaypoint(GameObject o)
    {
        if (o != null && o.GetComponent<BoxCollider>())
        {   
            Destroy(o.GetComponent<BoxCollider>());
        }
		Destroy(this);
    }
}
