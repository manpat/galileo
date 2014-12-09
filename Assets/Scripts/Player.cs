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
	public bool isPushing = false;

	public Transform sun;

	private Transform tr;
	private Rigidbody rb;
	private Animator  an;

	private float shadeAmount;

	void Awake () {
		tr = transform;
		rb = rigidbody;
		an = GetComponent<Animator>();
	}
	
	void Update () {
		CalcShadeAmount();

		Vector3 vel = rb.velocity;
		float s = isPushing?speedPushing:speed;

		vel.x = Input.GetAxis("Horizontal") * s;
		vel.z = Input.GetAxis("Vertical") * s;

		DoAnimations(vel/s);

		rb.velocity = vel;
		EffectsManager.main.SetDarkVignetteAlpha((1f - GetShadeAmount()) * vignetteShadeSensitivity);
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
		}else if(col.gameObject.CompareTag("Ship") && hasCollectedCompound){
			EventManager.main.OnReturnToShip();
		}else if(col.gameObject.CompareTag("Pushable")){
			isPushing = true;
		}
	}

	void OnCollisionStay(Collision col){
		if(col.gameObject.CompareTag("NotPushableAnyMore")){
			isPushing = false;
		}
	}

	void OnCollisionExit(Collision col){
		if(col.gameObject.CompareTag("NotPushableAnyMore")
			|| col.gameObject.CompareTag("Pushable")){
			isPushing = false;
		}
	}

	/*
	* idle 		0
	*
	* walk up	1
	* walk down	2
	* walk left	3
	* walk righ	4
	*
	* push up	5
	* push down	6
	* walk left	7
	* walk righ	8
	*/

	private int animNum = -1;

	void DoAnimations(Vector3 vel){
		bool dirty = false;
		vel.y = 0;
		if(vel.magnitude == 0f && animNum != 0){
			animNum = 0;
			dirty = true;

		}else{
			if(Mathf.Abs(vel.z) > Mathf.Abs(vel.x)){
				if(vel.z > 0f && animNum != 1){
					animNum = 1;
					dirty = true;
				}else if(vel.z < 0f && animNum != 2){
					animNum = 2;
					dirty = true;
				}
			}else{
				if(vel.x > 0f && animNum != 3){
					animNum = 3;
					dirty = true;
				}else if(vel.x < 0f && animNum != 4){
					animNum = 4;
					dirty = true;
				}
			}

			if(isPushing && animNum < 5){
				animNum += 4;
				dirty = true;
			}
		}

		if(dirty){
			tr.localScale = new Vector3(1,1,1);

			switch(animNum){
				case 0:
					an.Play("idle");
					break;
				case 1:
					an.Play("walk_up");
					break;
				case 2:
					an.Play("walk_down");
					break;
				case 3:
					an.Play("walk_horizontal");
					break;
				case 4:
					an.Play("walk_horizontal");
					tr.localScale = new Vector3(-1,1,1);
					break;

				case 5:
					an.Play("push_up");
					break;
				case 6:
					an.Play("push_down");
					break;
				case 7:
					an.Play("push_horizontal");
					break;
				case 8:
					an.Play("push_horizontal");
					tr.localScale = new Vector3(-1,1,1);
					break;
			}
		}
	}
}
