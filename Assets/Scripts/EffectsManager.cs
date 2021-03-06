﻿using UnityEngine;
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

	public Color sunColor;
	public Color preSunFlareColor;
	public Color sunFlareColor;

	public Color playerBurnColor;

	public float sunIntensity = 0.6f;
	public float sunIntensityBurning = 1f;

	private Color sunTargetColor;
	private float sunTargetIntensity;

	private float targetFade = 0f; 

	public Renderer darkenvignette;
	public Renderer lightenvignette;
	public Renderer fullscreenFade;
	public Light sunLight;

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
				Time.deltaTime*0.6f));

		sunLight.color = Color.Lerp(sunLight.color, sunTargetColor, Time.deltaTime);
		sunLight.intensity = Mathf.Lerp(sunLight.intensity, sunTargetIntensity, Time.deltaTime*0.1f);

		fullscreenFade.material.color = Color.Lerp(fullscreenFade.material.color, new Color(0f, 0f, 0f, targetFade), Time.deltaTime);
	}

	public Color startCol; 

	// a <= 0 no sun burn
	// a < 1 warn
	// a == 1 burn
	public void SetSun(float a){
		if(a <= 0f) {
			startCol = sunLight.color;
			sunTargetColor = sunColor;
			sunTargetIntensity = sunIntensity;
			return;
		}

		if(a < 0.5f){
			sunTargetColor = Color.Lerp(startCol, preSunFlareColor, a/0.5f);
		}else if(a < 1f){
			sunTargetColor = Color.Lerp(preSunFlareColor, sunFlareColor, (a-0.5f)*5f);
			sunTargetIntensity = sunIntensityBurning;	
		}else if(a >= 1f){
			sunTargetColor = (sunFlareColor);
		}
	}

	public void SetDarkVignetteAlpha(float a){
		targetColor = vignetteColor;
		targetColor.a = a;
	}
	public void SetLightVignetteColor(Color c){
		lightenvignette.material.SetColor("_TintColor", c);
	}
	public void SetFade(float a){
		targetFade = a;
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

	public void OnPlayerBurn(){
		SetLightVignetteColor(playerBurnColor);
	}
}
