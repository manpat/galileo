using UnityEngine;
using System.Collections;

public enum Sound {
	StartGame,
	ButtonClick,
	ScreenChange, // Whoosh

	BoulderMove,
	BoulderHit,

	FootstepRock,
	FootstepSand,
}

public enum Music {
	Menu,
	InGame,
	Credits,
}

public class EffectsManager : MonoBehaviour {
	static public EffectsManager main = null;

	public Color compoundCollectColor;
	public Color vignetteColor;

	public Renderer darkenvignette;
	public Renderer lightenvignette;

	private Color targetColor;

	void Awake(){
		if(main != null){
			Debug.LogError("Duplicate EffectsManager detected in " + name + "\nDestroying duplicate");
			Destroy(this);
			return;
		}		
		main = this;
	}

	void Update(){
		darkenvignette.material.color = Color.Lerp(darkenvignette.material.color, targetColor, Time.deltaTime);
		SetLightVignetteColor(
			Color.Lerp(
				lightenvignette.material.GetColor("_TintColor"), 
				new Color(1f, 1f, 1f, 0f), 
				Time.deltaTime*0.3f));
	}

	public void SetDarkVignetteAlpha(float a){
		targetColor = vignetteColor;
		targetColor.a = a;
	}
	public void SetLightVignetteColor(Color c){
		lightenvignette.material.SetColor("_TintColor", c);
	}

	// Plays the footstep sound that matches the terrain at (pos). 
	// Also creates a small particle effect.
	public void OnFootstepAt(Vector3 pos){

	}

	// Plays the compound collect sound and creates a screen effect. 
	// Also changes the background so that the next moon is in view.
	public void OnCompoundCollect(){
		SetLightVignetteColor(compoundCollectColor);
	}

	// Plays a 2D sound, (sound).
	public void PlaySound(Sound sound){

	}

	// Begins fade to the music track (music).
	public void SetMusic(Music music){

	}
}
