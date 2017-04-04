using UnityEngine;
using System.Collections;

public class TerrainMove : MonoBehaviour {

	[HideInInspector] public static float speed;

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
		if (gameController.isGameOver())
			speed = 0;
		
		rigidbody.velocity = new Vector2 (speed * -1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
