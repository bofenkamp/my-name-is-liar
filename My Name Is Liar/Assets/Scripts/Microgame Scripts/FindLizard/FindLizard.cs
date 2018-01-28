using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindLizard : MonoBehaviour {

	private List<KeyCode> p1keys;
	private List<KeyCode> p2keys;
	private List<KeyCode> keys;
	private List<string> wrongAnswers;

	public Text[] options;

	private Text goodButton;
	private KeyCode goodKey;

	private bool playing;

	void Start () {

	}

	void Update () {

		if (Input.GetKeyDown(goodKey) && playing)
			this.GetComponent<Microgame>().EndMicrogame(true);

		else if (this.GetComponent<Microgame> ().Owner != null && 
			this.GetComponent<Microgame> ().Owner.PlayerNumber == PlayerID.One) {

			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)
				|| Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
				this.GetComponent<Microgame>().EndMicrogame(false);

		} else if (playing) {

			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow)
				|| Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
				this.GetComponent<Microgame>().EndMicrogame(false);

		}

	}

	public void MakeQuiz () {

		p1keys = new List<KeyCode> ();
		p1keys.Add (KeyCode.A);
		p1keys.Add (KeyCode.S);
		p1keys.Add (KeyCode.D);
		p1keys.Add (KeyCode.W);

		p2keys = new List<KeyCode> ();
		p2keys.Add (KeyCode.LeftArrow);
		p2keys.Add (KeyCode.DownArrow);
		p2keys.Add (KeyCode.RightArrow);
		p2keys.Add (KeyCode.UpArrow);

		wrongAnswers = new List<string> (); 
		wrongAnswers.Add ("Canada");
		wrongAnswers.Add ("Some rocks, but not all of them");
		wrongAnswers.Add ("A burrito wearing makeup");
		wrongAnswers.Add ("The complete works of William Shakespeare");
		wrongAnswers.Add ("A lizard");
		wrongAnswers.Add ("A spider");
		wrongAnswers.Add ("A human lying about being a beetle");
		wrongAnswers.Add ("A shiny new lexus!!!");
		wrongAnswers.Add ("An inexplicable flag of Kyrgyzstan");
		wrongAnswers.Add ("Your butt");
		wrongAnswers.Add ("A person dressed as a beetle");

		int goodNumber = Random.Range (0, 4);

		if (this.GetComponent<Microgame> ().Owner != null) {

			if (this.GetComponent<Microgame> ().Owner.PlayerNumber == PlayerID.One)
				keys = p1keys;
			else
				keys = p2keys;

		} else
			keys = p2keys;

		goodKey = keys [Random.Range (0, keys.Count)];
		keys.Remove (goodKey);

		string goodKeyStr = "";

		if (goodKey == KeyCode.A)
			goodKeyStr = "A";
		else if (goodKey == KeyCode.S)
			goodKeyStr = "S";
		else if (goodKey == KeyCode.D)
			goodKeyStr = "D";
		else if (goodKey == KeyCode.W)
			goodKeyStr = "W";
		else if (goodKey == KeyCode.LeftArrow)
			goodKeyStr = "Left";
		else if (goodKey == KeyCode.DownArrow)
			goodKeyStr = "Down";
		else if (goodKey == KeyCode.RightArrow)
			goodKeyStr = "Right";
		else if (goodKey == KeyCode.UpArrow)
			goodKeyStr = "Up";

		options [goodNumber].text = goodKeyStr + ": Nupserha atriceps";

		for (int i = 0; i < 4; i++) {

			if (i != goodNumber) {

				KeyCode key = keys [Random.Range (0, keys.Count)];
				keys.Remove (key);

				string keyStr = "";

				if (key == KeyCode.A)
					keyStr = "A";
				else if (key == KeyCode.S)
					keyStr = "S";
				else if (key == KeyCode.D)
					keyStr = "D";
				else if (key == KeyCode.W)
					keyStr = "W";
				else if (key == KeyCode.LeftArrow)
					keyStr = "Left";
				else if (key == KeyCode.DownArrow)
					keyStr = "Down";
				else if (key == KeyCode.RightArrow)
					keyStr = "Right";
				else if (key == KeyCode.UpArrow)
					keyStr = "Up";

				string answer = wrongAnswers [Random.Range (0, wrongAnswers.Count)];
				wrongAnswers.Remove (answer);

				options [i].text = keyStr + ": " + answer;

			}

		}

		playing = true;

	}

}
