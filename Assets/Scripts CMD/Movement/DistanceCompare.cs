using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCompare : MonoBehaviour {

    CommandMove commandMove;
	DistanceCalc distanceCalc;
    public bool InRange;

    public delegate void DistanceCompareDelegate();
    public event DistanceCompareDelegate OutOfRange;
    public event DistanceCompareDelegate InsideRange;    

    public bool DistanceTest(CommandMove c, DistanceCalc d)
    {

        commandMove = c;
        distanceCalc = d;

        if (distanceCalc != null){
            if ((distanceCalc.currentDistance + distanceCalc.cumulativeDistance) < distanceCalc.maxDistance)
                {
                    InRange =true;
                    if (InsideRange != null){
                    InsideRange();
                    }
                }
            if ((distanceCalc.currentDistance + distanceCalc.cumulativeDistance) >= distanceCalc.maxDistance) 
                {
                    InRange=false;
                    if (OutOfRange != null){
                    OutOfRange();
                    }
                }
        }   
        return false;
    }

}
