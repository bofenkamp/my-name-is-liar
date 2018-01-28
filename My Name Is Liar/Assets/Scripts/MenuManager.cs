using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void StartGame() {
		//TODO
		SceneManager.LoadScene("Game");
	}

	public void MainMenu() {
		SceneManager.LoadScene ("Main_Menu");
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
