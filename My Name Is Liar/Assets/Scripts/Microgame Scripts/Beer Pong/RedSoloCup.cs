using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSoloCup : MonoBehaviour {

	[SerializeField]
	private float cupSpeed;
	[SerializeField] 
	private Microgame microgame;
	private BeerPongManager beerPongManager;
	private Rigidbody2D rb;

	private float minPosY;
	private bool playing = false;

	// Rendering/Transform initialization
	void Start () {
		beerPongManager = microgame.GetComponent<BeerPongManager> ();
		rb = GetComponent<Rigidbody2D> ();
			
		minPosY = transform.position.y;
	}

	// Called when game actually begins
	public void OnStartGame() {
		playing = true;
	}

	void Update () {
		if (!playing) {
			return;
		}

		Vector2 velo = Vector2.zero;

		// Horizontal movement
		float cupRadius = GetComponent<BoxCollider2D> ().size.x;
		float getAxis = microgame.Owner.GetAxis (PlayerAxis.Horizontal);
		if (!((getAxis < 0 && (transform.position.x - cupRadius / 2) < -beerPongManager.camWidth / 2)
		    || (getAxis > 0 && (transform.position.x + cupRadius / 2) > beerPongManager.camWidth / 2))) {
			velo.x = cupSpeed * getAxis;
		}

		// Vertical movement
		float height = GetComponent <BoxCollider2D> ().size.y * 2;
		getAxis = microgame.Owner.GetAxis (PlayerAxis.Vertical);
		if (!((getAxis < 0 && (transform.position.y) < minPosY)
			|| (getAxis > 0 && (transform.position.y) > minPosY + height / 2))) {
			velo.y = cupSpeed * getAxis;
		}

		rb.velocity = velo;
	}

	// Destroy the ping pong ball and subtract from timer when it falls into the cup
	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<PingPongBall> ()) {
			Destroy (other.gameObject);
			microgame.AddToTime (-beerPongManager.timerSubtraction);
		}
	}
}
