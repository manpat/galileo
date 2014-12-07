using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 5f;
	public float speedPushing = 2.5f;

	public float footstepInterval = 0.5f;

	public bool hasCollectedCompound = false;

	public Transform sun;

	private Transform tr;
	private Rigidbody rb;

	void Awake () {
		tr = transform;
		rb = rigidbody;
	}
	
	void Update () {
		Vector3 vel = rb.velocity;

		vel.x = Input.GetAxis("Horizontal") * speed;
		vel.z = Input.GetAxis("Vertical") * speed;

		rb.velocity = vel;

		print(CanSeeSun());
	}

	public bool CanSeeSun() {
		Vector3 rot = -sun.forward;

		return !Physics.Raycast(tr.position, rot);
	}
}
