using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlCounter : MonoBehaviour {

	public Image up;
	public Image down;

	public Sprite w;
	public Sprite s;

	private KeyCode more;
	private KeyCode less;
	private KeyCode confirm;
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
			confirm = KeyCode.D;

		} else {

			more = KeyCode.UpArrow;
			less = KeyCode.DownArrow;
			confirm = KeyCode.RightArrow;

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

		if (Input.GetKeyDown (confirm)) {
			float numGirls = controller.GetComponent<Girls> ().num;
			if (numGirls == num)
				controller.EndMicrogame (true);
			else
				controller.EndMicrogame (false);

		}
		
	}
}
