using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class JSONTest : MonoBehaviour {

	public	bool test1;
	public bool test2;
	public	List<string> Hotpants;

	public TextAsset tMoney;

	public string path;
	// Use this for initialization
	void Start () {

		LoadFromJSON(path);

	}

/* 		if (!File.Exists("e:\\temp\\thisThang.json"))
			{
				print("saving to JSON");
				File.WriteAllText("e:\\temp\\thisThang.json", SaveToJSON());
			}




/*     public static JSONPrep CreateFromJSON()
    {
        return JsonUtility.FromJson<JSONPrep>();
    } */

/* 	public void LoadGameData()
	
	{ 
		
	} */

/* 	public void JSONTEST(){

		JsonUtility.FromJson<JSONPrep>("e:\\temp\\thisThang.json");
	} */

	

	public void LoadFromJSON(string path){

		Debug.Log("loading from JSON");
		JsonUtility.FromJson<JSONPrep>(path);

	}

/* 	public string SaveToJSON()
	{
		return JsonUtility.ToJson(this);
	} */
}



[Serializable]
public class JSONPrep{
	public bool test1;
	public bool test2;
}
