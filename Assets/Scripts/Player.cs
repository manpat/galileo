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
		EffectsManager.main.SetVignetteAlpha((1f - GetShadeAmount()) * vignetteShadeSensitivity);
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

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Compound")){
			EventManager.main.OnCompoundCollect();
			EffectsManager.main.OnCompoundCollect();
			Destroy(col.gameObject);

			hasCollectedCompound = true;
		}
	}
}
