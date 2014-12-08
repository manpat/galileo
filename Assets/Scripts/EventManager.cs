using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	static public EventManager main = null;

	public GameObject Compound;

	void Awake(){
		if(main != null){
			Debug.LogError("Duplicate EventManager detected in " + name + "\nDestroying duplicate");
			Destroy(this);
			return;
		}		
		main = this;
	}

	// Changes the layout of the level as necessary. 
	// Sets a flag that reenables the players interaction with the ship.
	public void OnCompoundCollect(){

	}

	// Assumes the player has the compound. 
	// Will trigger a cutscene or transition and load either the next level or the credits.
	public void OnReturnToShip(){
		
	}
}
