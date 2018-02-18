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

	SaveToJSON();

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

		JSONPrep[] test = new JSONPrep[2];//JsonHelper.FromJson<JSONPrep>(File.ReadAllText(path));
	//	print(test);
		//List<JSONPrep> testLoad = test.ToList();
	
		test[0] = new JSONPrep();
		test[0].key = this.key;
		test[0].test1 = this.test1;
		test[0].test2 = this.test2;
		test[0].Hotpants = this.Hotpants;

		test[1] = new JSONPrep();
		test[1].key = this.key;
		test[1].test1 = this.test1;
		test[1].test2 = this.test2;
		test[1].Hotpants = this.Hotpants;

		//testLoad.Add(t);

		string jsonToSave = JsonHelper.ToJson(test.ToArray(), true); 

//		string jsonToSave = JsonUtility.ToJson(this, true);
		
		print (jsonToSave);
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
