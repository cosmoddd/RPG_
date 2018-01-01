using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCommandMove : CombatCommand  // the master controller for the 'Move' combat command.
{
    public delegate void CombatCommandMoveDelegate ();
    public event CombatCommandMoveDelegate Clicked;
    public static event CombatCommandMoveDelegate Move;

    public List<GameObject> navPointObject;

    public override void Start()
    {
        transform.localPosition = new Vector3(0,0,0);
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
           Clicked();
            return;
        }

        if (Input.GetKeyDown(KeyCode.G)&& (transform.GetComponent<NavWaypointMover>() == null)) // check if it's not already attached
        {
           this.gameObject.AddComponent<NavWaypointMover>();
           Move();  // execute the move event
        }
    }
}
