using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadMap ();
	}

	void LoadMap() {
		SceneManager.LoadScene ("Map", LoadSceneMode.Additive);
	}
}
