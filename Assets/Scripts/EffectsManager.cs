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

	void Awake(){
		if(main != null){
			Debug.LogError("Duplicate EffectsManager detected in " + name + "\nDestroying duplicate");
			Destroy(this);
			return;
		}		
		main = this;
	}

	// Plays the footstep sound that matches the terrain at (pos). 
	// Also creates a small particle effect.
	public void OnFootstepAt(Vector3 pos){

	}

	// Plays the compound collect sound and creates a screen effect. 
	// Also changes the background so that the next moon is in view.
	public void OnCompoundCollect(){

	}

	// Plays a 2D sound, (sound).
	public void PlaySound(Sound sound){

	}

	// Begins fade to the music track (music).
	public void SetMusic(Music music){

	}
}
