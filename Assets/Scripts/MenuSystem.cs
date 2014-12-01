using UnityEngine;
using System.Collections;

public class MenuSystem : MonoBehaviour {
	// static public MenuSystem main;

	// void Awake(){
	// 	main = this;
	// }

	static public void OnButton(ButtonType type){
		switch(type){
			case ButtonType.Start:
				Application.LoadLevel("level1");
				break;

			case ButtonType.Quit:
				Application.Quit();
				break;

			default:
				Debug.LogError("OnButton " + type.ToString() + " not implemented");
				break;
		}
	}
}
