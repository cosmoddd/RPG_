using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDestroyEverything : MonoBehaviour {


    public void _NavDestroyEverything(NavWaypointManager n){         //destroyes entire movement command hierarchy and starts from scratch

        n.commandMove.Clicked -= n.NavPointPlace;
        n.commandMove.RightClicked -= n.DeSpawn;
        n.commandMove.combatController.SelectEvent -= n.RemoveColliderFromWaypoint;
        CombatController.DeSelectAllEvent -= n.AddColliderToWaypoint;

            Destroy(n.lineRenderObject);
            Destroy(this.gameObject); 
    }
}
