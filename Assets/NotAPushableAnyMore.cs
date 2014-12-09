using UnityEngine;
using System.Collections;

public class NotAPushableAnyMore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Finish")){
			tag = "NotPushableAnyMore";
		}
	}
}
