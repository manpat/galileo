using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	public GameObject[] levelLayers;
	public int currentLayer = 0;

	// Use this for initialization
	void Start () {
		levelLayers[currentLayer].SetActive(true);
		levelLayers[1-currentLayer].SetActive(false);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		levelLayers[currentLayer].SetActive(false);
		currentLayer = 1-currentLayer;
		levelLayers[currentLayer].SetActive(true);
	}
}
