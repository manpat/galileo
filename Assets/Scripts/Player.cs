using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 5f;
	public float speedPushing = 2.5f;

	public float footstepInterval = 0.5f;

	public bool hasCollectedCompound = false;

	public Transform sun;
	public Renderer ren;

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

		// print(CanSeeSun());
		if(CanSeeSun()){
			ren.material.color = Color.red;
		}else{
			ren.material.color = Color.white;
		}
	}

	public bool CanSeeSun() {
		Vector3 rot = -sun.forward;

		return !Physics.Raycast(tr.position, rot);
	}
}
