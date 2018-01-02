using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetTheDistance : MonoBehaviour {

    public static float Calculate(NavMeshPath path) {
        
        if (path.corners.Length < 2)
            return 0;
        
        Vector3 previousCorner = path.corners[0];
        float actualDistance = 0.0F;
        int i = 1;
        while (i < path.corners.Length) {
            Vector3 currentCorner = path.corners[i];
            actualDistance += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return actualDistance;
    }
}
