using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESTest : MonoBehaviour {


	public bool test1;
	public bool test2;
	public string assmaster;

	// Use this for initialization
	void Start () {
		
		ES2.Save(test1,"funkyboss.txt");
		ES2.Save(test2,"funkyboss.txt");
		ES2.Save(assmaster,"funkyboss.txt");


	}
	
}
