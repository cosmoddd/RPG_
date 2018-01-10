using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeMover {

//    public static SubscribeMover subscribeMover;

	private static void Start()
    {
/*         if(subscribeMover != null)
        {
 //           SubscribeMover subscribeMover = new SubscribeMover();
        } */

        Debug.Log("awake");
      //  CombatCommandMove.Move += Initialize;
    }

    public static void Initialize(CommandMove c)
    {
        c.gameObject.AddComponent<NavWaypointMover>();
    }

}
