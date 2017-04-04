using UnityEngine;
using System.Collections;

public class RandomZRotator : MonoBehaviour {

	public Range maxZ;
	public float velocity = 2f;

	private float maxNow;
	private int dir = 0;

	void Start() {
		dir = 0;
		turn ();
	}

	// Use this for initialization
	void turn() {
		dir = (dir == 1 ? 0 : 1);
		//if (dir == 1) {
			maxNow = maxZ.getIntValue ();
		/*} else {
			maxNow = -maxZ.getIntValue ();
		}*/
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (dir == 1) {
			transform.Rotate (0, 0, +velocity);
			if (transform.rotation.eulerAngles.z >= maxNow)
				turn ();
		} else {
			transform.Rotate (0, 0, -velocity);
			if (360 - transform.rotation.eulerAngles.z <= maxNow)
				turn ();
		}
	}
}
