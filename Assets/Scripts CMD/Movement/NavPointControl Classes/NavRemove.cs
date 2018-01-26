using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavRemove : MonoBehaviour {
    
    public void DeSpawn(List<GameObject> navPointObjects, Vector3 v, GameObject navSpawned, GameObject lObj, LineRenderer l)      //checker for dynamically retracting the placed navpoints
    {

        if (navSpawned != null && (l.enabled == true))  // If there's a line but no point.
        {
            
            l.enabled = false;
            navSpawned.AddComponent<BoxCollider>();
            Destroy(this);
            return;
        }

        if (navSpawned != null && (l.enabled == false))  // If there's a point but no line.
        {
            Clear(navPointObjects, lObj, this.gameObject.GetComponent<NavWaypointManager>());
            l.enabled = true;
            Destroy(this);           
            return;
        }

        else{
            transform.gameObject.SendMessage("DeleteThis");
            Destroy(this);
        }

    }

    public void Clear(List<GameObject> navPointObjects,	GameObject lineRenderObject,NavWaypointManager navWaypointManager) // removes most recent nav points, sets parent of previous one
    {
    
        Destroy(navPointObjects[navPointObjects.Count-1]);
        navPointObjects.Remove(navPointObjects[navPointObjects.Count-1]);

        if (navPointObjects.Count - 1 > -1)
        {
            GameObject mostRecentNav = navPointObjects[navPointObjects.Count - 1];
            
            lineRenderObject.SendMessage("SetAgentSource", mostRecentNav);
            lineRenderObject = mostRecentNav;
            navWaypointManager.navPointPrefabSpawned = mostRecentNav;
        }
        else
        {
            lineRenderObject = transform.parent.GetComponentInChildren<PathwayDraw>().gameObject;
            lineRenderObject.SendMessage("SetAgentSource", this.transform.parent.parent.gameObject);
        }

        SendMessage("Subtract");
		Destroy(this);
    }
}
