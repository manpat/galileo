using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 5f;

	private Rigidbody rb;

	void Start () {
		rb = rigidbody;
	}
	
	void Update () {
		Vector3 vel = rb.velocity;

		vel.x = Input.GetAxis("Horizontal") * speed;
		vel.z = Input.GetAxis("Vertical") * speed;

		rb.velocity = vel;
	}
}
