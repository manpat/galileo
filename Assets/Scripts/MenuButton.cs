using UnityEngine;
using System.Collections;

public enum ButtonState {
	Idle,
	Hover,
	Down,
};

public enum ButtonType {
	None,
	Start,
	Quit,
};

public class MenuButton : MonoBehaviour {
	public ButtonType type;

	public float hoverScale = 0.1f;
	public float downScale = -0.1f;
	public float scaleRate = 0.1f; // % per s

	public ButtonState state = ButtonState.Idle;
	float startScale;
	float currentScale;

	// Use this for initialization
	void Start () {
		startScale = transform.localScale.x;
		currentScale = startScale;
	}
	
	// Update is called once per frame
	void Update () {
		float targetScale = startScale;
		switch(state){
			case ButtonState.Hover:
				targetScale *= (1f + hoverScale);
				break;
			case ButtonState.Down:
				targetScale *= (1f + downScale);
				break;
		}

		currentScale = targetScale; //Mathf.Lerp(currentScale, targetScale, Time.deltaTime * scaleRate);
		transform.localScale = Vector3.one * currentScale;
	}

	void OnMouseOver(){
		if(state != ButtonState.Down)
			state = ButtonState.Hover;
	}

	void OnMouseExit(){
		state = ButtonState.Idle;
	}

	void OnMouseDown(){
		state = ButtonState.Down;
	}
	void OnMouseUpAsButton(){
		state = ButtonState.Hover;
		MenuSystem.OnButton(type);
	}
}
