using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour {

	public GameObject enemyShot;
	public Transform shootSpawn;
	public Range wait;

	private int waitCount = 2;

	// Use this for initialization
	void Start () {
		waitCount = wait.getIntValue ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		waitCount = Mathf.Max (0, waitCount - 1);
		if (waitCount == 0) {
			Shoot ();
		}
	}

	void Shoot() {
		waitCount = wait.getIntValue ();
		//Debug.Log ("Shootin!");
		Instantiate (enemyShot, shootSpawn.position, shootSpawn.rotation);
	}
}
