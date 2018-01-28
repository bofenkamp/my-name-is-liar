using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void StartGame() {
		//SceneManager.LoadScene("Map");
		SceneManager.LoadScene ("Game", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync ("Main_Menu");
	}

	public void MainMenu() {
		SceneManager.LoadScene ("Main_Menu");
	}

	public void QuitGame() {
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        } else {
			Application.Quit ();
		}
	}
}
