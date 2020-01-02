using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathwaySegment : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
    public float segmentDistance;
    public float pathLengthTotal;
    public Vector3 markerPlacement;

    public MeshRenderer[] markerPrefabs;


    void Update()
    {
        SpawnSubsequentMarkers();
    }


    void SpawnSubsequentMarkers()
    {   
        
        for (int i = 0; i < markerPrefabs.Length; i++)
        {
           if ((segmentDistance * (i+1)) < PathLength(navMeshAgent.path))
           {
               Vector3 markerPlacement = new Vector3();
               markerPlacement = FindPointAlongPath(navMeshAgent.path.corners, segmentDistance*(i+1), markerPrefabs[i]);
               markerPrefabs[i].transform.position = markerPlacement + new Vector3(0,1,0);
           }

           if (((segmentDistance * (i+1)) > PathLength(navMeshAgent.path)))
           {
               markerPrefabs[i].enabled = false;
           }
        
        }
    }


    // get the accurate path length
    public float PathLength(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return 0;

        Vector3 previousCorner = path.corners[0];
        pathLengthTotal = 0.0F;
        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            pathLengthTotal += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return pathLengthTotal;
    } 


    Vector3 FindPointAlongPath(Vector3[] path, float distanceToTravel, MeshRenderer marker)
        {
            if(distanceToTravel < 0)
            {
                return path[0];
            }
     
            //Loop Through Each Corner in Path
            for (int i = 0; i < path.Length -1; i++)
            {
                //If the distance between the next to points is less than the distance you have left to travel
                if (distanceToTravel <= Vector3.Distance(path[i], path[i + 1]))
                {
                    //Calculate the point that is the correct distance between the two points and return it
                    Vector3 directionToTravel = path[i + 1] - path[i];
                    directionToTravel.Normalize();

                    marker.enabled = true;
                    return (path[i] + (directionToTravel * distanceToTravel));
                }
                else
                {
                    //otherwise subtract the distance between those 2 points from the distance left to travel
                    distanceToTravel -= Vector3.Distance(path[i], path[i + 1]);
                }
            }
     
            //if the distance to travel is greater than the distance of the path, return the final point
            marker.enabled = false;
            return path[path.Length - 1];

        }



}
