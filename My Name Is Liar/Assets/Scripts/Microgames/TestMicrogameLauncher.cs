using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMicrogameLauncher : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
			GameManager.Instance.LaunchMicrogame(PlayerID.Two, null);
        else if (Input.GetKeyDown(KeyCode.N))
			GameManager.Instance.LaunchMicrogame(PlayerID.One, null);
	}
}
