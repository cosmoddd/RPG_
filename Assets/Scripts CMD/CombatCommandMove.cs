using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatCommandMove : CombatCommand
{
    NavPointCreator nav;
    public List<Vector3> navPoint;

    public bool isPaused = false;
    public bool moving = false;

    public override void Start()
    {
        nav = (NavPointCreator)gameObject.AddComponent<NavPointCreator>();
        nav.DistanceSet += RoundDistance;
    }

    IEnumerator MoveToWaypoint()
    {
        for (int i = 0; i < navPoint.Count; i++)
        {
            Vector3 targetPosition = new Vector3(
                                        navPoint[i].x,
                                        gameObject.transform.parent.position.y,
                                        navPoint[i].z
                                        );
            while ((gameObject.transform.parent.position != targetPosition))
            {
                moving = true;
                gameObject.transform.parent.position = Vector3.MoveTowards(gameObject.transform.parent.position,
                                                                            targetPosition,
                                                                            (5 * Time.deltaTime));

                while (isPaused)
                {
                    moving = false;
                    yield return null;
                }

                yield return null;
            }
            yield return null;
        }

        moving = false;
        print("done moving");
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))// && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            nav.SetNav();
            navPoint.Add(nav.point1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.G) && (isPaused == false) && (moving == false))
        {
            StartCoroutine("MoveToWaypoint");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }
    }

    void RoundDistance(float d, Vector3 p)
    {
        slots = Mathf.RoundToInt(d);
        print("Distance Rounded= " + slots);
        AddToList();
        return;
    }

    public void UnSubscribe()
    {
        nav.DistanceSet -= RoundDistance;
    }

}
