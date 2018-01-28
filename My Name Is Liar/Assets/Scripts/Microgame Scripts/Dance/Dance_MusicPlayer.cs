using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_MusicPlayer : MonoBehaviour {

	public void StartMusic() {
		MusicManager.instance.PlayingDDR (true);
	}

	public void StopMusic() {
		MusicManager.instance.PlayingDDR (false);
	}
}
