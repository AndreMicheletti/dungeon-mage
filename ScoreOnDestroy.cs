using UnityEngine;
using System.Collections;

public class ScoreOnDestroy : MonoBehaviour {

	public int scoreValue = 0;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject controllerObj = GameObject.FindWithTag("GameController");
		if (controllerObj != null)
			gameController = controllerObj.GetComponent<GameController> ();

		if (gameController == null)
			Debug.Log ("Can't find game controller");
	}

	void OnDestroy() {
		gameController.addScore (scoreValue);
	}
}
