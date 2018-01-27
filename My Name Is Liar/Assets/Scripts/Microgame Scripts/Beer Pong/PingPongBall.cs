using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {

	[SerializeField]
	private float scalingSpeed;

	public static Microgame microgame;
	private BeerPongManager beerPongManager;
	private Rigidbody2D rb;
	private CircleCollider2D cc;

	private Vector3 baseScale;

	// Rendering/Transform initialization
	void Start () {
		beerPongManager = microgame.GetComponent<BeerPongManager> ();
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();

		baseScale = transform.localScale;
		transform.localScale = baseScale * 0.5f;
		Vector2 Impulse = new Vector2(Random.Range(-70f, 70f), Random.Range (300f, 500f));
		rb.AddForce (Impulse);
	}

	void Update () {
		if (transform.localScale.magnitude < baseScale.magnitude) {
			float scalingMultiplier = (transform.localScale.magnitude / baseScale.magnitude) * Time.deltaTime * scalingSpeed;
			transform.localScale += new Vector3(1, 1, 1) * scalingMultiplier;
		}

		if (!cc.enabled && rb.velocity.y < 0) {
			cc.enabled = true;
		} else if (transform.position.y < -beerPongManager.camHeight) {
			Destroy (gameObject);
		}

		// Bounce off edges
		if (((transform.position.x - cc.radius) < -beerPongManager.camWidth / 2 && rb.velocity.x < 0)
			|| ((transform.position.x + cc.radius) > beerPongManager.camWidth / 2 && rb.velocity.x > 0)) {
			rb.velocity = new Vector2(-1 * rb.velocity.x, rb.velocity.y);
		}
	}
}
