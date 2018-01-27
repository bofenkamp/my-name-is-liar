using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {

	[SerializeField]
	private float scalingSpeed;

	private Vector3 baseScale;

	// Rendering/Transform initialization
	void Start () {
		baseScale = transform.localScale;
		transform.localScale = baseScale * 0.3f;
		Vector2 Impulse = new Vector2(Random.Range(-70f, 70f), Random.Range (400f, 600f));
		GetComponent<Rigidbody2D> ().AddForce (Impulse);
	}

	void Update () {
		if (transform.localScale.magnitude < baseScale.magnitude) {
			transform.localScale += new Vector3 (1, 1, 1) * Time.deltaTime * scalingSpeed;
		}
	}
}
