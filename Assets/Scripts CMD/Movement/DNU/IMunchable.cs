using UnityEngine;
using UnityEngine.AI;

public interface IMunchable
{
	void burntToast();

	int maggotBrain {get;set;}

}

public class funky : MonoBehaviour,  IMunchable
{
	int blanket =3;

	public int maggotBrain {get;set;}

	public void burntToast()
	{
		Debug.Log("burning da toast");
	}

/* 	
	
	int boobies(int b)
	{
		b += 3;
		return b;	
	} */
}

public abstract class DipThong 
{

	public IMunchable munchable {get; set;}

	public void yams()
	{
		munchable.maggotBrain = 3;

		thong = munchable.maggotBrain;
	}


	public int thong;
	public int vampire;
	bool struggla;
}
