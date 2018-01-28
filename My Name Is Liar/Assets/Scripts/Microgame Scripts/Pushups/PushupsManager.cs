using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PushupPosition {Up, Down}

public class PushupsManager : MonoBehaviour {

    [SerializeField]
    private int _PushupsRequired = 14;
    [SerializeField]
    private int _PushupVariance = 6;

	[SerializeField]
	private SpriteRenderer _Man, _Meter, _Marker, _Range;
	[SerializeField]
	private Sprite _UpPosition, _DownPosition;

	private Microgame _microgame;
	private bool playing = false;

	private PushupPosition position = PushupPosition.Up;
    private int _PushupsRemaining;

	// Paramters
	private float scaleUpSpeed = 150f;
	private float scaleDownSpeed = 60f;
	private float rangeRadius = 6f;
	private float pushupTime = 0.4f;
	private float meterHeightMult = 0.046f;

	private float scale;
	private float rangeLocation;
	private float pushupCooldown;

	// Use this for initialization
	void Start () {
		_microgame = GetComponent<Microgame>();

		_Man.sprite = _UpPosition;

		randomizeRange();
        _PushupsRemaining = _PushupsRequired + Random.Range(0, _PushupVariance);
	}

	// Called when game actually begins
	public void OnStartGame() {
		playing = true;
	}

	void Update() {
		if (!playing) {
			return;
		}

		// Player input to change scale
		float getAxis = _microgame.Owner.GetAxis (PlayerAxis.Vertical);
		if (getAxis > 0) {
			if (scale < 100) {
				scale += scaleUpSpeed * Time.deltaTime;
			}
		} else {
			if (scale > 0) {
				scale -= scaleDownSpeed * Time.deltaTime;
			}
		}
		_Marker.transform.position = _Meter.transform.position + new Vector3 (0f, (scale - 50f) * meterHeightMult, -2f); 

		if (rangeLocation - rangeRadius <= scale && scale <= rangeLocation + rangeRadius) {
			doPushups ();
		}
		pushupCooldown -= Time.deltaTime;
		maybeRandomizeRange (0.0025f);
	}

	void doPushups() {
		if (pushupCooldown <= 0) {
			switchPosition ();
			pushupCooldown = pushupTime;
		}
	}

	void switchPosition() {
		if (position == PushupPosition.Up) {
			position = PushupPosition.Down;
			_Man.sprite = _DownPosition;
		} else {
			position = PushupPosition.Up;
			_Man.sprite = _UpPosition;
		}

		_PushupsRemaining--;
		if (_PushupsRemaining <= 0 && position == PushupPosition.Down) {
			_microgame.EndMicrogame(true);
		}

		maybeRandomizeRange (0.3f);
	}

	void maybeRandomizeRange(float chance) {
		float roll = Random.Range (0f, 1f);
		if (roll <= chance) {
			randomizeRange ();
		}
	}

	void randomizeRange() {
		rangeLocation = Random.Range(7f, 94f);
		_Range.transform.position = _Meter.transform.position + new Vector3(0f, (rangeLocation - 50f) * meterHeightMult, -1f);
	}
}
