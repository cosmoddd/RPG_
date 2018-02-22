using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavRemove : MonoBehaviour {
    
    GameObject mostRecentNav;


    public void DeSpawn(List<GameObject> navPointObjects, Vector3 v, GameObject navSpawned, GameObject lObj, LineRenderer l)      //checker for dynamically retracting the placed navpoints
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering over UI 
        {
            Clear(navPointObjects, lObj, this.gameObject.GetComponent<NavWaypointManager>());
            if (mostRecentNav != null && mostRecentNav.GetComponent<BoxCollider>() == null)
                {
                    print("collider added");
                    print(mostRecentNav.name);
                    mostRecentNav.AddComponent<BoxCollider>();
                }
            Destroy(this);
            return;          
        }

        if ((navSpawned != null && (l.enabled == true)))  // RIGHT CLICK -- If there's a line but no point 
        {           
            l.enabled = false;
            navSpawned.AddComponent<BoxCollider>();
            Destroy(this);
            return;
        }

        if (navSpawned != null && (l.enabled == false))  // RIGHT CLICK -- If there's a point but no line.
        {
            Clear(navPointObjects, lObj, this.gameObject.GetComponent<NavWaypointManager>());
            l.enabled = true;
            Destroy(this);           
            return;
        }

        else{
            l.enabled = false;
            Destroy(this);
            return;
        }

    }

    public void Clear(List<GameObject> navPointObjects,	GameObject lineRenderObject,NavWaypointManager navWaypointManager) // removes most recent nav points, sets parent of previous one
    {
    
        Destroy(navPointObjects[navPointObjects.Count-1]);
        navPointObjects.Remove(navPointObjects[navPointObjects.Count-1]);

        if (navPointObjects.Count - 1 > -1)                                     // if the list is not empty
        {
            mostRecentNav = navPointObjects[navPointObjects.Count - 1];
            
            lineRenderObject.SendMessage("SetAgentSource", mostRecentNav);
            lineRenderObject = mostRecentNav;
            navWaypointManager.navPointPrefabSpawned = mostRecentNav;
        }
        else                                                                     // if the list is empty (for some reason)
        {
            lineRenderObject = transform.parent.GetComponentInChildren<PathwayDraw>().gameObject;
            lineRenderObject.SendMessage("SetAgentSource", this.transform.parent.parent.gameObject);
        }
        SendMessage("Subtract");
		Destroy(this);
    }
}
