using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	static public EventManager main = null;

	void Awake(){
		if(main != null){
			Debug.LogError("Duplicate EventManager detected in " + name + "\nDestroying duplicate");
			Destroy(this);
			return;
		}		
		main = this;
	}

	// Changes the layout of the level as necessary. 
	public void OnCompoundCollect(){

	}

	// Assumes the player has the compound. 
	// Will trigger a cutscene or transition and load either the next level or the credits.
	public void OnReturnToShip(){

	}
}
