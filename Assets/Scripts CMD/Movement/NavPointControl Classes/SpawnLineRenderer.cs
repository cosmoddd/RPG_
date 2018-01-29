using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnLineRenderer : MonoBehaviour{

    public GameObject spawnLineRenderer(NavWaypointManager n, GameObject target, bool enabledAtStart)  // activates line renderer script on Nav Point
    {
        GameObject l = Object.Instantiate(n.lineRendererPrefab, target.transform.position, Quaternion.identity);  //spawn new line renderer
        l.transform.SetParent(this.transform.parent);            // keeps the parent as the source
        l.AddComponent<PathwayActivate>();
        LineRenderer line = l.GetComponent<LineRenderer>();
        line.enabled = enabledAtStart;
        l.SendMessage("SetAgentSource", target);  
        Destroy(this); 
        return l;
    }

}