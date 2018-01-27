using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Microgame : MonoBehaviour {
    
    public Player Owner { get; private set; }

    [NonSerialized]
    public float Timescale = 1.0f;

    [SerializeField]
    private Camera _Camera;
    [SerializeField]
    private float _InitialTimeRemaining = 10;
    [SerializeField]
    private GameEvent _OnStartGame;
    [SerializeField]
    private GameEvent _OnEndGame;

    private float _TimeRemaining;

    private void Start()
    {
        GameManager.Instance.RegisterMicrogame(this);
    }

    public void StartMicrogame(Player player) {
        if (Owner != null)
            return;
        
        Owner = player;
        _TimeRemaining = _InitialTimeRemaining;
        _Camera.targetTexture = player.MicrogameTexture;

        int layer = 0;
        if (player.PlayerNumber == PlayerID.One)
            layer = 8;
        else
            layer = 9;
        _Camera.cullingMask = 1 << layer;

        var objs = gameObject.scene.GetRootGameObjects();
        foreach (var obj in objs)
            SetLayerRecursive(obj, layer);

        _OnStartGame.Invoke();
    }

    private void SetLayerRecursive(GameObject go, int layer) {
        go.layer = layer;
        foreach (Transform t in go.transform)
            SetLayerRecursive(t.gameObject, layer);
    }

    public void EndMicrogame() {
        // TODO
        GameManager.Instance.DeregisterMicrogame(this);
        _OnEndGame.Invoke();
        _Camera.enabled = false;
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    public void AddToTime(float secs) {
        _TimeRemaining += secs;
    }

    [Serializable]
    private class GameEvent : UnityEvent {}

}
