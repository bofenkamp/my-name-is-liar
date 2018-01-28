using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundFX {winMinigame, failMinigame}

public class SoundFX_Manager : MonoBehaviour {

	public static SoundFX_Manager instance;

	[SerializeField]
	private AudioClip winMinigame, failMinigame;

	private AudioSource AS;

	// Use this for initialization
	void Start () {
		AS = GetComponent<AudioSource> ();

		if (instance) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (this);
		}
	}
	
	public void playClip(SoundFX clip) {
		if (clip == SoundFX.winMinigame) {
			AS.PlayOneShot (winMinigame);
		} else if (clip == SoundFX.failMinigame) {
			AS.PlayOneShot (failMinigame);
		}
	}
}
