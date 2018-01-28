using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicTracks {None, MenuTheme, Overworld, DDR}

public class MusicManager : MonoBehaviour {

	public static MusicManager instance;

	[SerializeField]
	private AudioClip MenuTheme, Overworld, DDR;

	[SerializeField]
	private AudioSource DDR_Source;
	private int DDR_counter = 0;

	private AudioSource AS;

	// Use this for initialization
	void Start () {
		AS = GetComponent<AudioSource> ();

		if (instance) {
			if (instance.AS.clip != AS.clip && AS.playOnAwake) {
				instance.AS.clip = AS.clip;
				instance.AS.Play ();
			}
			if (!AS.playOnAwake) {
				instance.AS.Stop ();
			}
			instance.AS.clip = AS.clip;
			instance.AS.loop = AS.loop;
			instance.DDR_Source = DDR_Source;
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (this);
		}
	}

	public void SwitchMusic(MusicTracks newTrack) {
		if (newTrack == MusicTracks.None) {
			AS.Stop ();
		} else if (newTrack == MusicTracks.MenuTheme) {
			AS.clip = MenuTheme;
			AS.Play ();
		} else if (newTrack == MusicTracks.Overworld) {
			AS.clip = Overworld;
			AS.Play ();
		} else if (newTrack == MusicTracks.DDR) {
			AS.clip = DDR;
			AS.Play ();
		}
	}

	public void PlayingDDR(bool playing) {
		if (playing) {
			DDR_counter++;
		} else {
			DDR_counter--;
		}

		if (DDR_counter >= 1) {
			AS.Pause ();
			DDR_Source.Play ();
		} else if (DDR_counter == 0) {
			DDR_Source.Stop ();
			AS.Play ();
		}
	}
}
