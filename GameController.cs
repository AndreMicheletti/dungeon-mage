using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Range {
	public float min, max;

	public float getValue() {
		return Random.Range (min, max);
	}

	public int getIntValue() {
		return Random.Range ((int)min, (int)max);
	}
}

public class GameController : MonoBehaviour {

	public AudioSource BGM;

	public GameObject[] terrains;
	public Transform terrainCheck;
	public LayerMask terrainLayer;
	public float startTerrainSpeed = 4.5f;
	public float maximumTerrainSpeed = 6.6f;


	public GameObject[] hazards;
	public int enemyScoreValue = 20;
	public Range firstSpawnDelay;
	public Range spawnDelay;
	public Range vertVariation;
	public Transform hazardSpawn;

	public int distanceScoreTimer = 100;
	public GameObject gameOverText;
	public Text scoreText;

	public AudioSource dieSound;

	private bool gameOver = false;
	private float hazardTimer = 0;
	private int distanceCounter = 0;
	private int score = 0;

	private int distanceScoreValue = 5;
	private int speedUpTimer = 0;


	// Use this for initialization
	void Start () {
		hazardTimer = firstSpawnDelay.getIntValue ();
		score = 0;
		distanceCounter = distanceScoreTimer;
		distanceScoreValue = 5;
		TerrainMove.speed = startTerrainSpeed;
		BGM.pitch = 1f;
	}

	public void DoGameOver() {
		gameOver = true;
		gameOverText.SetActive (true);
		dieSound.Play ();
		GameObject player = GameObject.FindWithTag ("Player");
		if (player != null)
			player.GetComponent<Rigidbody2D> ().isKinematic = true;
	}

	public bool isGameOver() {
		return gameOver;
	}

	public void addScore(int value) {
		score += value;
		scoreText.text = "" + score;
	}

	public void addEnemyScore() {
		addScore (enemyScoreValue);
	}

	public int getScore() {
		return this.score;
	}

	void FixedUpdate() {

		if (!Physics2D.OverlapCircle(terrainCheck.position, 0.1f, terrainLayer)) {
			SpawnTerrain ();
		}

		if (gameOver == true) {
			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
				Application.LoadLevel ("Main");
			}
			return;
		}

		if (hazardTimer > 0f) {
			hazardTimer -= 1f;
			hazardTimer = Mathf.Max (hazardTimer, 0);
		}

		if (hazardTimer == 0f) {
			SpawnHazards ();
		}

		if (distanceCounter > 0)
			distanceCounter -= 1;
		else {
			distanceCounter = distanceScoreTimer;
			addScore (distanceScoreValue);
			speedUpTimer += 1;
		}

		//Debug.Log (TerrainMove.speed);

		if (speedUpTimer >= 5) {
			speedUpTimer = 0;
			distanceScoreValue += 3;
			TerrainMove.speed += 0.2f;
			if (TerrainMove.speed > maximumTerrainSpeed)
				TerrainMove.speed = maximumTerrainSpeed;
		}
	}

	void SpawnHazards() {
		hazardTimer = spawnDelay.getIntValue ();
		int selected = Random.Range (0, hazards.Length);
		Instantiate (hazards[selected], hazardSpawn.position, hazardSpawn.rotation);
		hazardSpawn.transform.position = new Vector2 (hazardSpawn.position.x, vertVariation.getValue ());
	}

	void SpawnTerrain() {
		int selected = Random.Range (0, terrains.Length);
		Vector2 position = new Vector2 (terrainCheck.position.x - 0.5f, terrainCheck.position.y);
		Instantiate (terrains[selected], position, terrainCheck.rotation);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
