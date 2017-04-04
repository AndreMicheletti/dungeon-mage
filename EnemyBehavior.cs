using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float minY;
	public float maxY;
	public float horzSpeed = 1f;
	public float vertSpeed = 4f;

	private float direction = 1;
	private new Rigidbody2D rigidbody;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		GameObject controllerObj = GameObject.FindWithTag("GameController");
		if (controllerObj != null)
			gameController = controllerObj.GetComponent<GameController> ();

		if (gameController == null)
			Debug.Log ("can't find game controller");
	}

	void FixedUpdate() {
		if (gameController.isGameOver ()) {
			rigidbody.velocity = new Vector2 (0, 0);
			rigidbody.isKinematic = true;
			return;
		}
		
		if (transform.position.y <= minY || transform.position.y >= maxY)
			direction *= -1;

		rigidbody.velocity = new Vector2 (-(horzSpeed + TerrainMove.speed), vertSpeed * direction);
	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			gameController.DoGameOver ();
			rigidbody.velocity = new Vector2 (0, 0);
			rigidbody.isKinematic = true;
		}
	}
}
