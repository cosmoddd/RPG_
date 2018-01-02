using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetThePoint : MonoBehaviour {

    public static Vector3 PickVector3()

    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                Vector3 point = interactionInfo.point;
                return point;
            }
        else return Vector3.zero;
    }

}