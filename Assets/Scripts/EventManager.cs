using UnityEngine;
using System.Collections;


public class EventManager : MonoBehaviour {
	static public EventManager main = null;

	public WaypointThing fallingPlatform;
	public float platformMoveSpeed;
	public float sunBurnInterval = 6f;
	public float sunBurnWarnLength = 2f;
	public float sunBurnLength = 3f;

	private float platformMoveTimer = 0f;
	private bool isCompoundCollected = false;

	private float sunBurnTimer = -2f;
	private bool burning = false;

	void Awake(){
		if(main != null){
			Debug.LogError("Duplicate EventManager detected in " + name + "\nDestroying duplicate");
			Destroy(this);
			return;
		}		
		main = this;
	}

	void Update(){
		if(isCompoundCollected){
			platformMoveTimer += Time.deltaTime;
			Vector3 p0 = fallingPlatform.savedPositions[0];
			Vector3 p1 = fallingPlatform.savedPositions[1];
			Vector3 diff = (p1 - p0);
			float d = diff.magnitude;

			fallingPlatform.transform.position = Interp(p0, p1, platformMoveTimer/d);
		}

		sunBurnTimer += Time.deltaTime;
		if(sunBurnTimer >= sunBurnLength){
			sunBurnTimer = -sunBurnInterval;
		}

		burning = false;
		if(sunBurnTimer > 0f){
			EffectsManager.main.SetSun(1f);
			burning = true;
		}else if(sunBurnTimer > -sunBurnWarnLength){
			EffectsManager.main.SetSun((sunBurnTimer + sunBurnWarnLength) / sunBurnWarnLength);
		}else{
			EffectsManager.main.SetSun(-1f);
		}
	}

	public void DoPlayerStuff(Player p){
		if(p.GetShadeAmount() < 0.4f && burning){
			p.Burn();
		}
	}

	// Changes the layout of the level as necessary. 
	public void OnCompoundCollect(){
		isCompoundCollected = true;
		platformMoveTimer = -2f;
	}

	// Assumes the player has the compound. 
	// Will trigger a cutscene or transition and load either the next level or the credits.
	public void OnReturnToShip(){

	}

	private Vector3 Interp(Vector3 a, Vector3 b, float x){
		x = Mathf.Clamp01(x);
		x = x*x*x;
		return Vector3.Lerp(a, b, x);
	}
}
