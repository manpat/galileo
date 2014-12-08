using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 5f;
	public float speedPushing = 2.5f;

	public float footstepInterval = 0.5f;
	public float shadeSampleDist = 0.4f;
	public int shadeSampleCount = 5;
	public float vignetteShadeSensitivity = 0.7f;

	public bool hasCollectedCompound = false;

	public Transform sun;
	public Renderer ren;
	public Renderer vignette;

	private Transform tr;
	private Rigidbody rb;

	private float shadeAmount;

	void Awake () {
		tr = transform;
		rb = rigidbody;
	}
	
	void Update () {
		CalcShadeAmount();

		Vector3 vel = rb.velocity;

		vel.x = Input.GetAxis("Horizontal") * speed;
		vel.z = Input.GetAxis("Vertical") * speed;

		rb.velocity = vel;

		vignette.material.color = Color.Lerp(vignette.material.color, new Color(1f, 1f, 1f, (1f - GetShadeAmount())*vignetteShadeSensitivity), Time.deltaTime);
	}

	public float GetShadeAmount(){
		return shadeAmount;
	}

	private void CalcShadeAmount() {
		Vector3 rot = -sun.forward;
		shadeAmount = 0f;

		float sampleIncrement = 2f*shadeSampleDist / (float)shadeSampleCount;

		for(float x = -shadeSampleDist; x <= shadeSampleDist; x += sampleIncrement){
			Vector3 samplePos = tr.position + Vector3.right * x;
			shadeAmount += Physics.Raycast(samplePos, rot) ? 1f : 0f;
		}

		shadeAmount /= (float)shadeSampleCount;
		shadeAmount = 1f - shadeAmount;
	}
}
