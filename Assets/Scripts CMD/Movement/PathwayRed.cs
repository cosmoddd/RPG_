using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathwayRed : MonoBehaviour {

	CommandMove commandMove;
	// Use this for initialization
	void Start () {
	commandMove = transform.parent.GetComponent<CommandMove>();
    commandMove.InsideRange += InsideRange;
    commandMove.OutOfRange += OutOfRange;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InsideRange(
){

    print("in range");
}

void OutOfRange(){

    
    print("out of range");
}
}
