using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlCounter : MonoBehaviour {

	public SpriteRenderer up;
	public SpriteRenderer down;

	public Sprite w;
	public Sprite s;

	private KeyCode more;
	private KeyCode less;
	public Microgame controller;

	private int num = 0;
	private Text text;

	// Use this for initialization
	void Start () {

		if (controller.Owner != null && controller.Owner.PlayerNumber == PlayerID.One) {

			up.sprite = w;
			down.sprite = s;
			more = KeyCode.W;
			less = KeyCode.S;

		} else {

			more = KeyCode.UpArrow;
			less = KeyCode.DownArrow;

		}

		text = this.GetComponent<Text> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (more)) {
			num++;
			text.text = num.ToString ();
		}

		if (Input.GetKeyDown (less)) {
			num -= 1;
			text.text = num.ToString ();
		}
		
	}
}
