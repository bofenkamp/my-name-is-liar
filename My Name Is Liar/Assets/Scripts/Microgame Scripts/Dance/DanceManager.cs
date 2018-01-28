using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceManager : MonoBehaviour {

    [SerializeField]
    private Microgame _Microgame;
    [SerializeField]
    private Transform _UpSpawn;
    [SerializeField]
    private Transform _DownSpawn;
    [SerializeField]
    private Transform _LeftSpawn;
    [SerializeField]
    private Transform _RightSpawn;
    [SerializeField]
    private Transform _ArrowPrefab;

    [SerializeField]
    private int _MissesAllowed = 5;
    [SerializeField]
    private float _MinSpeed = 2;
    [SerializeField]
    private float _MaxSpeed = 4;
    [SerializeField]
    private float _MinInterval = 0.5f;
    [SerializeField]
    private float _MaxInterval = 1.5f;
    [SerializeField]
    private float _SpawnDelay = 1.5f;
    [SerializeField]
    private float _GameLength = 8.5f;

    [SerializeField]
    private float _ArrowTravelDistance = 3;

    private float _StartTime;
    private int _Misses = 0;
    private float _NextSpawnTime;

    public void OnHit(DanceArrow arr) {
        Destroy(arr.gameObject);
    }

    public void OnMiss(DanceArrow arr) {
        Destroy(arr.gameObject);

        _Misses++;
        if (_Misses >= _MissesAllowed)
            _Microgame.EndMicrogame(false);
    }

    private void Start()
    {
        _StartTime = Time.time;
        _NextSpawnTime = _StartTime + _SpawnDelay;
    }

    private void Update () {
        if(Time.time > _NextSpawnTime) {
            Direction d = (Direction)Random.Range(0, 4);

            Vector3 spawn = Vector3.zero;
            switch(d) {
                case Direction.Right:
                    spawn = _RightSpawn.position;
                    break;
                case Direction.Left:
                    spawn = _LeftSpawn.position;
                    break;
                case Direction.Up:
                    spawn = _UpSpawn.position;
                    break;
                case Direction.Down:
                    spawn = _DownSpawn.position;
                    break;
            }

            var go = Instantiate(_ArrowPrefab, spawn, Quaternion.identity).gameObject;
            _Microgame.OnInstantiateObject(go);

            var arr = go.GetComponent<DanceArrow>();
            arr.Cardinality = d;
            arr.Speed = Random.Range(_MinSpeed, _MaxSpeed);
            arr.Game = _Microgame;
            arr.Manager = this;
            arr.DistanceToTravel = _ArrowTravelDistance;

            _NextSpawnTime = Time.time + Random.Range(-_MaxInterval, _MaxInterval);
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_UpSpawn.transform.position, _UpSpawn.transform.position + Vector3.down * _ArrowTravelDistance);
    }
}

public enum Direction {
    Right = 0, Down = 3, Left = 2, Up = 1
}