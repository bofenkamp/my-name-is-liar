using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LizardTextSizing : MonoBehaviour {

	public Text[] TextBoxes;

	private bool adjusted;

	void Update () {
		if (adjusted) {
			return;
		}

		// Check if text has resized to best fit
		foreach (Text textBox in TextBoxes) {
			int size = textBox.cachedTextGenerator.fontSizeUsedForBestFit;
			if (size != 0) {
				adjustAll ();
				return;
			}
		}
	}

	void adjustAll() {
		// Find minimum size
		int minSize = int.MaxValue;
		foreach (Text textBox in TextBoxes) {
			int size = textBox.cachedTextGenerator.fontSizeUsedForBestFit;
			if (size < minSize) {
				minSize = size;
			}
		}

		// Set all to minimum size
		foreach (Text textBox in TextBoxes) {
			textBox.fontSize = minSize;
			textBox.resizeTextForBestFit = false;
		}

		adjusted = true;
	}
}
