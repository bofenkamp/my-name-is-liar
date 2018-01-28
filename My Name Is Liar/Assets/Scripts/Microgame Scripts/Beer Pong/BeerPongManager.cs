using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerPongManager : MonoBehaviour {

	[SerializeField]
	private float _PPBSpawnInterval, _timerSubtraction;
	public GameObject PPBPrefab;

	private Microgame microgame;

	[HideInInspector]
	public float camHeight, camWidth;

	public float timerSubtraction{
		get { return _timerSubtraction; }
	}

	// Use this for initialization
	void Start () {
		microgame = GetComponent<Microgame>();

		// Get Main Camera height and width
		camHeight = microgame.Camera.orthographicSize * 2f;
		camWidth = camHeight * Camera.main.aspect;
	}

	// Called when game actually begins
	public void OnStartGame() {
		InvokeRepeating ("spawnNewPPB", 0.3f, _PPBSpawnInterval);
	}
	
	void spawnNewPPB() {
		GameObject newPPB = Instantiate (PPBPrefab);

		microgame.OnInstantiateObject (newPPB);
		newPPB.GetComponent<PingPongBall>().beerPongManager = this;

		float spawnX = 0.9f * Random.Range (-camWidth/2, camWidth/2);
		newPPB.transform.position = new Vector3 (spawnX, 0f);
	}
}
