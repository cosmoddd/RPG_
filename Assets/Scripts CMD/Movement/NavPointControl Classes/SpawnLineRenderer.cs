using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnLineRenderer : MonoBehaviour{

    public GameObject _SpawnLineRenderer(NavWaypointManager n, GameObject target)  // activates line renderer script on Nav Point
    {
        GameObject l = Object.Instantiate(n.lineRendererPrefab, target.transform.position, Quaternion.identity);  //spawn new line renderer
        l.transform.SetParent(this.transform.parent);            // keeps the parent as the source
        l.GetComponent<PathwayDraw>().SetAgentSource(target);   
        Destroy(this);  // this works, really?!?1?
        return l;
    }

}