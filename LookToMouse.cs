using UnityEngine;
using System.Collections;

public class LookToMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		Vector3 p2 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 p1 = transform.position;
		float angle = Mathf.Atan2 (p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
		transform.rotation = Quaternion.Euler (0, 0, angle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
