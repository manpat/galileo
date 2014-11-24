using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public GameObject target;
	public float attackAngle; // Degrees
	public float distance;

	private Transform tr;

	void Awake(){
		tr = transform;
	}

	void Update () {
		tr.rotation = Quaternion.Euler(attackAngle, 0, 0);

		float rad = attackAngle * Mathf.Deg2Rad;

		// y = d sin ϴ
		// z = d cos ϴ
		tr.position = target.transform.position + new Vector3(
			0f, Mathf.Sin(rad), -Mathf.Cos(rad)
			) * distance;
	}
}
