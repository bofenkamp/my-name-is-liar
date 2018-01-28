using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGirl : MonoBehaviour {

	public Vector2 targetPos;
	public Vector2 velo;
	private Girls control;
	public float speed;

	// Use this for initialization
	void Start () {

		control = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Girls> ();
		speed = Random.Range (control.speedMin, control.speedMax);
		NewDest ();
		
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().velocity = velo;
		
	}

	void NewDest() {

		float x = Random.Range (control.minX, control.maxX);
		float y = Random.Range (control.minY, control.maxY);
		targetPos = new Vector2 (x, y);
		Vector2 pos = transform.position;
		Vector2 diff = targetPos - pos;
		float angle = Mathf.Atan2 (diff.y, diff.x);
		float veloX = speed * Mathf.Cos (angle);
		float veloY = speed * Mathf.Sin (angle);
		velo = new Vector2 (veloX, veloY);
		Invoke ("NewDest", diff.magnitude / speed);

	}
}
