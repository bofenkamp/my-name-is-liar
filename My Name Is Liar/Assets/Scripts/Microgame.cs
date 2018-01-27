using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

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

    public void StartMicrogame(Player player) {
        if (Owner != null)
            return;
        
        Owner = player;
        _TimeRemaining = _InitialTimeRemaining;
        _Camera.targetTexture = player.MicrogameTexture;
        _OnStartGame.Invoke();
    }

    public void EndMicrogame() {
        // TODO
    }

    public void AddToTime(float secs) {
        _TimeRemaining += secs;
    }

    [Serializable]
    private class GameEvent : UnityEvent {}

}
