using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public ParticleSystem runParticles;
	public float limitX = 0f;
	public float jumpForce = 100f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	public Transform shootSpawn;
	public int shootWaitTime = 20;
	public int runWait = 61;
	public GameObject fireShot;
	public GameObject arcaneShot;

	private new Rigidbody2D rigidbody;
	private bool grounded = false;
	private float groundCheckRadius = 0.2f;

	private GameController gameController;
	private Animator animator;


	private int shootCounter = 0;
	private int runCounter = 0;
	private float stayX;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		stayX = transform.position.x;

		GameObject controllerObj = GameObject.FindWithTag("GameController");
		if (controllerObj != null)
			gameController = controllerObj.GetComponent<GameController> ();

		if (gameController == null)
			Debug.Log ("Can't find game controller");
	}

	void Update() {
		if (runParticles.isPlaying && grounded == false) {
			runParticles.Stop ();
		}
		if (gameController.isGameOver())
			return;
		checkInputs ();
	}

	void checkGrounded() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		animator.SetBool ("Grounded", grounded);
	}

	// Fixed Update is called in a fixed timestamp
	void FixedUpdate() {

		checkGrounded ();

		// Return if Game Over
		if (gameController.isGameOver())
			return;

		if (shootCounter > 0)
			shootCounter -= 1;

		if (runParticles.isPlaying == false && (TerrainMove.speed >= (gameController.maximumTerrainSpeed / 1.2f))) {
			runParticles.Play ();
		}

		//Debug.Log (transform.position.x < stayX);
		if (transform.position.x < stayX) {
			if (runCounter == 0) {
				runCounter = runWait;
			} else {
				runCounter = Mathf.Max (1, runCounter - 1);
				if (runCounter == 1)
					rigidbody.velocity = new Vector2 (1f, rigidbody.velocity.y);
				else
					rigidbody.velocity = new Vector2 (0f, rigidbody.velocity.y);					
			}
		} else {
			runCounter = 0;
			rigidbody.velocity = new Vector2 (0f, rigidbody.velocity.y);
		}

		if (transform.position.x < limitX) {
			gameController.DoGameOver ();
			return;
		}

	}

	void checkInputs() {
		checkGrounded ();
		if (Input.GetAxisRaw("Fire1") > 0 && canShoot ()) {
			ShootFire ();
		}
		if (Input.GetAxisRaw("Fire2") > 0 && canShoot ()) {
			ShootArcane ();
		}
		if (Input.GetKeyDown(KeyCode.Space) && grounded) {
			Jump ();
		}
	}

	void Jump() {
		rigidbody.AddForce (new Vector2(0, jumpForce));
	}

	void ShootFire() {
		shootCounter = shootWaitTime;
		Instantiate (fireShot, shootSpawn.position, shootSpawn.rotation);
	}

	void ShootArcane() {
		shootCounter = shootWaitTime;
		Instantiate (arcaneShot, shootSpawn.position, shootSpawn.rotation);
	}

	bool canShoot() {
		if (shootCounter == 0) return true;
		return false;
	}

	void OnDestroy() {
		if (gameController != null)
			if (gameController.isGameOver() == false)
				gameController.DoGameOver ();
	}
}
