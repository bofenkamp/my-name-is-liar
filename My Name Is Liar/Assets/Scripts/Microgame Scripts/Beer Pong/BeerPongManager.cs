using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerPongManager : MonoBehaviour {

	[SerializeField]
	private float PPBSpawnInterval;
	public GameObject PPBPrefab;

	private float camHeight;
	private float camWidth;

	// Use this for initialization
	void Start () {
		// Get Main Camera height and width
		camHeight = Camera.main.orthographicSize * 2f;
		camWidth = camHeight * Camera.main.aspect;
		// For testing:

	}

	// Called when game actually begins
	public void OnStartGame() {
		InvokeRepeating ("spawnNewPPB", 0.3f, PPBSpawnInterval);
	}
	
	void spawnNewPPB() {
		GameObject newPPB = Instantiate (PPBPrefab);
		float spawnX = Random.Range (-camWidth, camWidth);
		newPPB.transform.position = new Vector3 (spawnX, 0f);
	}
}
