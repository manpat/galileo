using UnityEngine;
using System.Collections;

public class CompoundCollect : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	/*void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")  
		{
			GameObject.Find("Player").GetComponent<hasCollectedCompound>().t();
			audio.Play ();
			GameObject.Find("GodObject").GetComponent<EventManager>().OnCompoundCollect();
		}
	}*/
}
