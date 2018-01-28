using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameFeedback : MonoBehaviour {

	public static MinigameFeedback instance;

	[SerializeField]
	private float successDisplayTime, failureDisplayTime;

	[SerializeField]
	private TextMeshProUGUI goodFeedbackP1, badFeedbackP1, goodFeedbackP2, badFeedbackP2;

	private List<string> goodFeedback;
	private List<string> badFeedback;

	// Use this for initialization
	void Start () {
		if (instance) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (this);
		}

		disableFeedbackP1 ();
		disableFeedbackP2 ();

		// Populating feedback responses
		goodFeedback = new List<string> (); 
		goodFeedback.Add ("Awesome!");
		goodFeedback.Add ("Sick dude!");
		goodFeedback.Add ("Woah! Nice!");

		badFeedback = new List<string> ();
		badFeedback.Add ("That was lame");
		badFeedback.Add ("What was that, man...");
		badFeedback.Add ("So much for the \"Party King\"");
	}
	
	public void feedback(PlayerID playerNum, bool good) {
		if (playerNum == PlayerID.One && good) {
			goodFeedbackP1.text = randomFeedback(good);
			goodFeedbackP1.gameObject.SetActive (true);
			Invoke ("disableFeedbackP1", successDisplayTime);
		} else if (playerNum == PlayerID.One && !good) {
			badFeedbackP1.text = randomFeedback(good);
			badFeedbackP1.gameObject.SetActive (true);
			Invoke ("disableFeedbackP1", failureDisplayTime);
		} else if (playerNum == PlayerID.Two && good) {
			goodFeedbackP2.text = randomFeedback(good);
			goodFeedbackP2.gameObject.SetActive (true);
			Invoke ("disableFeedbackP2", successDisplayTime);
		} else if (playerNum == PlayerID.Two && !good) {
			badFeedbackP2.text = randomFeedback(good);
			badFeedbackP2.gameObject.SetActive (true);
			Invoke ("disableFeedbackP2", failureDisplayTime);
		}
	}

	public void disableFeedbackP1() {
		goodFeedbackP1.gameObject.SetActive (false);
		badFeedbackP1.gameObject.SetActive (false);
	}

	public void disableFeedbackP2() {
		goodFeedbackP2.gameObject.SetActive (false);
		badFeedbackP2.gameObject.SetActive (false);
	}

	public string randomFeedback(bool good) {
		if (good) {
			return goodFeedback [Random.Range (0, goodFeedback.Count)];
		} else {
			return badFeedback [Random.Range (0, badFeedback.Count)];
		}
	}
}
