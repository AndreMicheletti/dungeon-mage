using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	public float maxSpeed = 10f;
	public string targetTag;
	float angle;
	private GameController gameController;

	public GameObject impactSound;

	// Use this for initialization
	void Start () {
		angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;
		GameObject controllerObj = GameObject.FindWithTag("GameController");
		if (controllerObj != null)
			gameController = controllerObj.GetComponent<GameController> ();

		if (gameController == null)
			Debug.Log ("Can't find game controller");
	}

	void Update() {
		Vector3 pos = transform.position;
		pos.x += (Mathf.Cos (angle) * maxSpeed) * Time.deltaTime;
		pos.y += (Mathf.Sin (angle) * maxSpeed) * Time.deltaTime;
		transform.position = pos;
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == targetTag) {
			if (targetTag != "Player") {
				gameController.addEnemyScore ();
			}
			Destroy (other.gameObject);
			Destroy (gameObject);

			Instantiate (impactSound, this.transform.position, this.transform.rotation);

		} else {
			//Debug.Log (LayerMask.LayerToName(other.gameObject.layer));
			Destroy (gameObject);
			Instantiate (impactSound, this.transform.position, this.transform.rotation);
		}
	}
}
