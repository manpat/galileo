using UnityEngine;
using System.Collections;

public class TEMPPLAYERSCRIPT : MonoBehaviour {
	public float speed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = Vector3.zero;

		vel.x = Input.GetAxis("Horizontal") * speed;
		vel.y = Input.GetAxis("Vertical") * speed;

		rigidbody2D.velocity = vel;
	}
}
