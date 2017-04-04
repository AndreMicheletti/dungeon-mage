using UnityEngine;
using System.Collections;

public class TitleGhost : MonoBehaviour {

	public Range y;
	public float speed = 1f;
	public bool up = true;

	private Rigidbody2D me;
	// Use this for initialization
	void Start () {
		me = GetComponent<Rigidbody2D> ();
		if (up)
			me.velocity = new Vector2 (0, speed);
		else
			me.velocity = new Vector2 (0, -speed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (up) {
			if (transform.position.y >= y.max) {
				up = false;
			} else {
				me.velocity = new Vector2 (0, speed);
			}
		} else {
			if (transform.position.y <= y.min) {
				up = true;
			} else {
				me.velocity = new Vector2 (0, -speed);
			}
		}
	}
}
