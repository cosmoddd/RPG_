using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class JSONTest : MonoBehaviour {

	public string key;	
	public	bool test1;
	public bool test2;
	public	List<string> Hotpants;

	public string path;

	public int plasma;

	void Start () {

	//	LoadKey();
	
	SavePrefs();

	}

	public void SavePrefs()
	{
		PlayerPrefs.SetInt("funky business", plasma);
		PlayerPrefs.Save();
		print("saved");
	}

	public void LoadKey(){

		string s = File.ReadAllText(path);
		JSONPrep[] container = JsonHelper.FromJson<JSONPrep>(s);
		foreach (JSONPrep j in container)
		{
 		if (j.key == this.key)
			{
				print (j.key + " loaded.");
				test1 = j.test1;
				test2 = j.test2;
				Hotpants = j.Hotpants;
				return;
			}
		}
		print ("not equal!");
	}

	public void LoadFromJSON(string p)
		{

			Debug.Log("loading from JSON");
			JsonUtility.FromJsonOverwrite(p, this);
		
		}



	public string SaveToJSON()
	{
/* 
		JSONPrep[] test = JsonHelper.FromJson<JSONPrep>(File.ReadAllText(path));
		print(test);
		List<JSONPrep> testLoad = test.ToList();
	
		JSONPrep t = new JSONPrep();
		t.key = this.key;
		t.test1 = this.test1;
		t.test2 = this.test2;
		t.Hotpants = this.Hotpants;

		testLoad.Add(t);

		string jsonToSave = JsonHelper.ToJson(testLoad.ToArray(), true);  */

		string jsonToSave = JsonUtility.ToJson(this, true);
		
//		print (jsonToSave);
		return jsonToSave;
	}
}

[Serializable]
public class JSONPrep{
	public string key;
	public bool test1;
	public bool test2;
	public List <string> Hotpants;
}
