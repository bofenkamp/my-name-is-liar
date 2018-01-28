using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteDirection {left, right}

public class DanceOffManager : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer _YellowDancingImage, _BlueDancingImage;
    [SerializeField]
    private int _HitsRequired = 30;
    [SerializeField]
    private int _HitsVariance = 10;

	private SpriteRenderer _currentImage;
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
			_currentImage = _YellowDancingImage;
		} else {
			_currentImage = _BlueDancingImage;
		}
		_currentImage.gameObject.SetActive (true);

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
			_currentImage.flipX = !_currentImage.flipX;
			_currentDirection = SpriteDirection.left;
            _HitsRemaining--;
		} else if (getAxis > 0 && _currentDirection == SpriteDirection.left) {
			_currentImage.flipX = !_currentImage.flipX;
			_currentDirection = SpriteDirection.right;
            _HitsRemaining--;
		}

        if(_HitsRemaining <= 0) {
            _microgame.EndMicrogame(true);
        }
	}
}
