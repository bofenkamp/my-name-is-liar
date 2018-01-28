using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteDirection {left, right}

public class DanceOffManager : MonoBehaviour {

	[SerializeField]
	private float _timerSubtraction;
	[SerializeField]
	private SpriteRenderer _YellowDancingImage, _BlueDancingImage;
	private SpriteRenderer _currentImage;
	private SpriteDirection _currentDirection;

	private Microgame _microgame;
	private bool playing = false;

	[HideInInspector]
	public float camHeight, camWidth;

	public float timerSubtraction{
		get { return _timerSubtraction; }
	}

	// Use this for initialization
	void Start () {
		_microgame = GetComponent<Microgame>();
		_currentDirection = SpriteDirection.right;

		if (_microgame.Owner.PlayerNumber == PlayerID.One) {
			_currentImage = _YellowDancingImage;
		} else {
			_currentImage = _BlueDancingImage;
		}
		_currentImage.gameObject.SetActive (true);

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
			_currentImage.flipX = !_currentImage.flipX;
			_currentDirection = SpriteDirection.left;
			_microgame.AddToTime (-timerSubtraction);
		} else if (getAxis > 0 && _currentDirection == SpriteDirection.left) {
			_currentImage.flipX = !_currentImage.flipX;
			_currentDirection = SpriteDirection.right;
			_microgame.AddToTime (-timerSubtraction);
		}
	}
}
