using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMostRecentPoint : MonoBehaviour {

    public void Yams()
    {
        //navPointPrefabSpawned
    }

    public void Clear(List<GameObject> navPointObjects,
								GameObject lineRenderObject,
								DrawPathway drawPathway,
								NavWaypointManager navWaypointManager,
								LineRenderer lineRenderer,
								CommandMove combatCommandMove )             // removes most recent nav point (WIP)
    {
    
        Destroy(navPointObjects[navPointObjects.Count-1]);
        navPointObjects.Remove(navPointObjects[navPointObjects.Count-1]);
        print("it's " + (navPointObjects.Count));

        if (navPointObjects.Count - 1 > -1)
        {
            GameObject mostRecentNav = navPointObjects[navPointObjects.Count - 1];
            lineRenderObject = mostRecentNav;                   
            drawPathway.SetAgentSource(mostRecentNav);
            navWaypointManager.navPointPrefabSpawned = mostRecentNav;
        }
        else
        {
            lineRenderObject = transform.parent.GetComponentInChildren<DrawPathway>().gameObject;               
            drawPathway.SetAgentSource(this.transform.parent.parent.gameObject);
        }

        lineRenderer.enabled = true;
        combatCommandMove.ready = true;
        SendMessage("Subtract");
		Destroy(this);
    }
}
