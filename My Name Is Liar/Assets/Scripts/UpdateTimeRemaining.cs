using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTimeRemaining : MonoBehaviour {

    private TextMeshProUGUI _Text;

	void Start () {
        _Text = GetComponent<TextMeshProUGUI>();
	}
	
	void Update () {
        int secs = (int)GameManager.Instance.TimeRemaining;
        TimeSpan t = TimeSpan.FromSeconds(secs);

        string answer = string.Format("{0:D2}:{1:D2}",
                        t.Minutes,
                        t.Seconds);
        _Text.text = answer;
	}
}
