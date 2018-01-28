using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceArrow : MonoBehaviour {

    public float Speed { get; set; }
    public float DistanceToTravel { get; set; }
    public float SweetspotInterval { get; set; }

    public Direction Cardinality {
        set {
            transform.rotation = Quaternion.Euler(0, 0, 90f * (int)value);
            _Cardinality = value;
        }
        get {
            return _Cardinality;
        }
    }
    private Direction _Cardinality = Direction.Right;

    [HideInInspector]
    public Microgame Game;
    [HideInInspector]
    public DanceManager Manager;

    private float _StartTime;
    private Vector3 _StartPos;

    private void Start()
    {
        _StartTime = Time.time;
        _StartPos = transform.position;
    }

    void Update ()
    {
        float elapsed = Time.time - _StartTime;
        transform.position = _StartPos + Vector3.down * Speed * elapsed;

        float perfect_time = DistanceToTravel / Speed;
        if (Mathf.Abs(perfect_time - elapsed) <= SweetspotInterval)
        {
            float hoz = Game.Owner.GetAxis(PlayerAxis.Horizontal);
            float vrt = Game.Owner.GetAxis(PlayerAxis.Vertical);

            bool hit = (_Cardinality == Direction.Right && hoz > 0.5f) ||
                (_Cardinality == Direction.Left && hoz < -0.5f) ||
                (_Cardinality == Direction.Up && vrt > 0.5f) ||
                (_Cardinality == Direction.Down && vrt < -0.5f);
            if(hit) {
                Manager.OnHit(this);
            }
        }

        if (elapsed > perfect_time + SweetspotInterval + 1)
            Manager.OnMiss(this);
	}
}
