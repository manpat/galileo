using UnityEngine;
using System.Collections;

public class NotAPushableAnyMore : MonoBehaviour {
	public GameObject thingToFuckUp;

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Finish")){
			tag = "NotPushableAnyMore";
			if(thingToFuckUp){
				Destroy(thingToFuckUp);
			}
		}
	}
}
