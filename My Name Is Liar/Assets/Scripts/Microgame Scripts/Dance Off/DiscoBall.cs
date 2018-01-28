using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoBall : MonoBehaviour {

	[SerializeField]
	private Sprite[] sprites, BG_sprites;
	[SerializeField]
	private Image BG_Image;

	private SpriteRenderer sr;

	private float animTime = 0.3f;
	private float timer;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
		timer = animTime;
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			nextSprite ();
			timer = animTime;
		}
	}

	void nextSprite() {
		for (int i = 0; i < 3; i++) {
			if (sr.sprite == sprites [i]) {
				sr.sprite = sprites [(i + 1)%3];
				i = 3;
			}
		}
		for (int i = 0; i < 2; i++) {
			if (BG_Image.sprite == BG_sprites [i]) {
				BG_Image.sprite = BG_sprites [(i + 1) % 2];
				i = 2;
			}
		}
	}
}
