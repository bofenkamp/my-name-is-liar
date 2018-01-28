using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girls : MonoBehaviour {

	public int min;
	public int max;

	public int boyMin;
	public int boyMax;

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	public float speedMin;
	public float speedMax;

	[HideInInspector] public int num;

	public GameObject girl;
	public GameObject boy;

	[HideInInspector] public bool gameStarted;

	private Microgame micrograme;

	// Use this for initialization
	void Start () {
		micrograme = GetComponent<Microgame> ();
	}

	public void SpawnGirls () {
		
		micrograme = GetComponent<Microgame> ();

		num = Random.Range (min, max + 1);

		for (int i = 0; i < num; i++) {

			float x = Random.Range (minX, maxX);
			float y = Random.Range (minY, maxY);
			Vector2 pos = new Vector2 (x, y);
			GameObject PartyGirl = Instantiate (girl, pos, Quaternion.identity);
			micrograme.OnInstantiateObject (PartyGirl);
		}

		int boyNum = Random.Range (boyMin, boyMax + 1);

		for (int i = 0; i < boyNum; i++) {

			float x = Random.Range (minX, maxX);
			float y = Random.Range (minY, maxY);
			Vector2 pos = new Vector2 (x, y);
			GameObject DistractingBoi = Instantiate (boy, pos, Quaternion.identity);
			micrograme.OnInstantiateObject (DistractingBoi);

		}

		gameStarted = true;

	}
}
