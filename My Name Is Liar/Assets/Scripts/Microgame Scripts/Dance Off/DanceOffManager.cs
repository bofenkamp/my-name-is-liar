using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteDirection {left, right}

public class DanceOffManager : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer _YellowDancingImage, _BlueDancingImage;
	[SerializeField]
	private Sprite yellowSprite1, yellowSprite2, blueSprite1, blueSprite2;
    [SerializeField]
    private int _HitsRequired = 30;
    [SerializeField]
    private int _HitsVariance = 10;

	private SpriteDirection _currentDirection;

	private Microgame _microgame;
	private bool playing = false;

    private int _HitsRemaining;

	[HideInInspector]
	public float camHeight, camWidth;

	// Use this for initialization
	void Start () {
		_microgame = GetComponent<Microgame>();
		_currentDirection = SpriteDirection.right;

		if (_microgame.Owner.PlayerNumber == PlayerID.One) {
			_YellowDancingImage.gameObject.SetActive (true);
		} else {
			_BlueDancingImage.gameObject.SetActive (true);
		}

        _HitsRemaining = _HitsRequired + Random.Range(0, _HitsVariance);

		// Get Main Camera height and width
		camHeight = _microgame.Camera.orthographicSize * 2f;
		camWidth = camHeight * Camera.main.aspect;
	}

	// Called when game actually begins
	public void OnStartGame() {
		playing = true;
	}

	void Update() {
		if (!playing) {
			return;
		}

		float getAxis = _microgame.Owner.GetAxis (PlayerAxis.Horizontal);
		if (getAxis < 0 && _currentDirection == SpriteDirection.right) {
			flipImage ();
			_currentDirection = SpriteDirection.left;
            _HitsRemaining--;
		} else if (getAxis > 0 && _currentDirection == SpriteDirection.left) {
			flipImage ();
			_currentDirection = SpriteDirection.right;
            _HitsRemaining--;
		}

        if(_HitsRemaining <= 0) {
            _microgame.EndMicrogame(true);
        }
	}

	void flipImage() {
		if (_microgame.Owner.PlayerNumber == PlayerID.One) {
			if (_YellowDancingImage.sprite == yellowSprite1) {
				_YellowDancingImage.sprite = yellowSprite2;
			} else {
				_YellowDancingImage.sprite = yellowSprite1;
			}
		} else {
			if (_BlueDancingImage.sprite == blueSprite1) {
				_BlueDancingImage.sprite = blueSprite2;
			} else {
				_BlueDancingImage.sprite = blueSprite1;
			}
		}
	}
}
