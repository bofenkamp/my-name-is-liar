using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSoloCup : MonoBehaviour {

	[SerializeField]
	private float cupSpeed, timerSubtraction;
	[SerializeField] 
	private Microgame microgame;
	private BeerPongManager beerPongManager;
	private bool playing = false;

	// Rendering/Transform initialization
	void Start () {
		beerPongManager = microgame.GetComponent<BeerPongManager> ();
	}

	// Called when game actually begins
	public void OnStartGame() {
		playing = true;
	}

	void Update () {
		if (playing) {
			float cupRadius = GetComponent<BoxCollider2D> ().size.x;
			float getAxis = microgame.Owner.GetAxis (PlayerAxis.Horizontal);
			if ((getAxis < 0 && (transform.position.x - cupRadius / 2) < -beerPongManager.camWidth / 2)
				|| (getAxis > 0 && (transform.position.x + cupRadius / 2) > beerPongManager.camWidth / 2)) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (1f, 0f) * cupSpeed * getAxis;
			}
		}
	}

	// Destroy the ping pong ball and subtract from timer when it falls into the cup
	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<PingPongBall> ()) {
			Destroy (other.gameObject);
			microgame.AddToTime (-timerSubtraction);
		}
	}
}
