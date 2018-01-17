using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDestroyEverything : MonoBehaviour {


    public void _NavDestroyEverything(NavWaypointManager n){         //destroyes entire movement command hierarchy and starts from scratch

        n.commandMove.Clicked -= n.NavPointPlace;
        n.commandMove.RightClicked -= n.DeSpawn;

            Destroy(n.lineRenderObject);
            Destroy(this.gameObject); 
    }
}
