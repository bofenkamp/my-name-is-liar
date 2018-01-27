using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnKeypress : MonoBehaviour {

    [SerializeField]
    private Microgame _Game;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            _Game.EndMicrogame();
	}
}
