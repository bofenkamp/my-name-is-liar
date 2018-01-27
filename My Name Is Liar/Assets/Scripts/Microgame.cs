using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Microgame : MonoBehaviour {
    
    public Player Owner { get; private set; }

    public float Timescale {
        get {
            return _Timescale;
        }
        set {
            ApplyTimescale();
            _Timescale = value;
        }
    }
    private float _Timescale = 1.0f;
    public float TimeElapsed {
        get {
            return _AppliedTimeElapsed + (Time.time - _LastTimerApply) * Timescale;
        }
    }
    public float TimeRemaining {
        get {
            return Mathf.Max(0, _GameLength - TimeElapsed);
        }
    }

    [SerializeField]
    private Camera _Camera;
	public Camera Camera {
		get { return _Camera; }
	}
    [SerializeField]
    private float _InitialTimeRemaining = 10;
    [SerializeField]
    private string _GameName = "Do this!";
    [SerializeField]
    private GameEvent _OnStartGame;
    [SerializeField]
    private GameEvent _OnEndGame;
	[SerializeField]
	private bool _WinOnTimeOut = true;

    private float _AppliedTimeElapsed = 0;
    private float _LastTimerApply;
    private float _GameLength;
    private bool _HasStartedGame = false;

    private void Start()
    {
        GameManager.Instance.RegisterMicrogame(this);
    }

    private void Update() {
        if(_HasStartedGame) {
            Owner.TimeRemaining.text = Mathf.Ceil(TimeRemaining).ToString("0");

            if (TimeRemaining <= 0)
				EndMicrogame(_WinOnTimeOut);
        }
    }

    public void StartMicrogame(Player player) {
        if (Owner != null)
            return;
        
        Owner = player;
        _GameLength = _InitialTimeRemaining;
        _LastTimerApply = Time.time;
        _Camera.targetTexture = player.MicrogameTexture;
        _HasStartedGame = true;

        int layer = 0;
        if (player.PlayerNumber == PlayerID.One)
            layer = 8;
        else
            layer = 9;
        _Camera.cullingMask = 1 << layer;

        Owner.MicrogameTitle.text = _GameName;

        var objs = gameObject.scene.GetRootGameObjects();
        foreach (var obj in objs)
            SetLayerRecursive(obj, layer);

        _OnStartGame.Invoke();
    }

    private void ApplyTimescale() {
        _AppliedTimeElapsed += (_LastTimerApply - Time.time) * Timescale;
        _LastTimerApply = Time.time;
    }

    private void SetLayerRecursive(GameObject go, int layer) {
        go.layer = layer;
        foreach (Transform t in go.transform)
            SetLayerRecursive(t.gameObject, layer);
    }
		
    public void OnInstantiateObject(GameObject go) {
        int layer = 0;
        if (Owner.PlayerNumber == PlayerID.One)
            layer = 8;
        else
            layer = 9;
        SetLayerRecursive(go, layer);
    } 

	public void EndMicrogame(bool won) {
		
		Owner.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		//		GameManager.Instance.npcs [(int)Owner.PlayerNumber].GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

		//change the npc's opinion based on results
		if (won)
			GameManager.Instance.npcs[(int)Owner.PlayerNumber].GetComponent<NPC>().ChangeOpinion((int)Owner.PlayerNumber + 1, 1);
		else
			GameManager.Instance.npcs[(int)Owner.PlayerNumber].GetComponent<NPC>().ChangeOpinion((int)Owner.PlayerNumber + 1, -1);

		//let the NPC and player move again
		GameManager.Instance.npcs [(int)Owner.PlayerNumber].GetComponent<NPC> ().inMinigame = false;
		GameManager.Instance.npcs [(int)Owner.PlayerNumber].GetComponent<NPC> ().AllowMovement ();
		Owner.inMicrogame = false;
		
        // TODO
        GameManager.Instance.DeregisterMicrogame(this);
        _OnEndGame.Invoke();
        _Camera.enabled = false;
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    public void AddToTime(float secs) {
        _GameLength += secs;
    }

    [Serializable]
    private class GameEvent : UnityEvent {}

}
