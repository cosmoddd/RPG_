using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDestroyEverything : MonoBehaviour {


    public void navDestroyEverything(NavWaypointManager n){         //destroyes entire movement command hierarchy and starts from scratch

        n.commandMove.Clicked -= n.NavPointPlace;
        n.commandMove.RemoveWaypoint -= n.DeSpawn;
        n.commandMove.combatController.SelectEvent -= n.RemoveColliderFromWaypoint;
        CombatController.DeSelectAllEvent -= n.AddColliderToWaypoint;

        CombatController.DeSelectAllEvent += n.AddColliderToWaypoint;
        n.commandMove.combatController.SelectEvent += n.RemoveColliderFromWaypoint;

            Destroy(n.lineRenderObject);
            Destroy(this.gameObject); 
    }
}
