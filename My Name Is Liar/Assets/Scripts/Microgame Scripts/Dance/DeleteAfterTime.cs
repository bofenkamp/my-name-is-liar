using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour {

    [SerializeField]
    private float _DestroyAfterTime = 1f;

    private float _StartTime;

	void Start () {
        _StartTime = Time.time;
	}
	
	void Update () {
        if (Time.time - _StartTime > _DestroyAfterTime)
            Destroy(gameObject);
	}
}
