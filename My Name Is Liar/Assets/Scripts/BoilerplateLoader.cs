﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoilerplateLoader : MonoBehaviour {

	void Start () {
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
	}

}