using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerPongManager : MonoBehaviour {

	[SerializeField]
	private float PPBSpawnInterval;
	public GameObject PPBPrefab;

	private Microgame microgame;

	[HideInInspector]
	public float camHeight, camWidth;

	// Use this for initialization
	void Start () {
		microgame = GetComponent<Microgame>();
		PingPongBall.microgame = microgame;

		// Get Main Camera height and width
		camHeight = microgame.camera.orthographicSize * 2f;
		//camHeight = Camera.main.orthographicSize * 2f;
		camWidth = camHeight * Camera.main.aspect;
	}

	// Called when game actually begins
	public void OnStartGame() {
		InvokeRepeating ("spawnNewPPB", 0.3f, PPBSpawnInterval);
	}
	
	void spawnNewPPB() {
		GameObject newPPB = Instantiate (PPBPrefab);
		microgame.OnInstantiateObject (newPPB);
		float spawnX = 0.9f * Random.Range (-camWidth/2, camWidth/2);
		newPPB.transform.position = new Vector3 (spawnX, 0f);
	}
}
