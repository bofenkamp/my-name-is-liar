using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSoloCup : MonoBehaviour {

	[SerializeField]
	private float cupSpeed;
	[SerializeField] 
	private Microgame microgameManager;
	private bool playing = false;

	// Rendering/Transform initialization
	void Start () {
		
	}

	// Called when game actually begins
	public void OnStartGame() {
		playing = true;
	}

	void Update () {
		if (playing) {
			float getAxis = microgameManager.Owner.GetAxis (PlayerAxis.Horizontal);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (1f, 0f) * cupSpeed * getAxis;
		}
	}

	// Destroy the ping pong ball when it falls into the cup
	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<PingPongBall> ()) {
			Destroy (other.gameObject);
			// Lower timer here
		}
	}
}
