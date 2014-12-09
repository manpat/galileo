using UnityEngine;
using System.Collections;

public class CreditCamera : MonoBehaviour {
	public float moveSpeed = 1f;

	private WaypointThing wt;
	private float timer = 0f;

	void Awake(){
		wt = GetComponent<WaypointThing>();
	}
	
	void Update () {
		timer += Time.deltaTime;

		Vector3 p0 = wt.savedPositions[0];
		Vector3 p1 = wt.savedPositions[1];

		transform.position = Vector3.Lerp(p0, p1, timer*moveSpeed);

		if(timer*moveSpeed >= 1f){
			OnComplete();
		}
	}

	void OnComplete(){
		Application.LoadLevel("start");
	}
}
