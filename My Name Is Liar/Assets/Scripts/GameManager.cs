using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager singleton.  No need to put this in any scene.
    public static GameManager Instance
    {
        get
        {
            if (_Instance != null)
                return _Instance;

            var go = new GameObject("Game Manager");
            DontDestroyOnLoad(go);
            _Instance = go.AddComponent<GameManager>();
            return _Instance;
        }
    }
    private static GameManager _Instance;

    private Player[] _Players;
    private Microgame[] _LoadedMicrogames;
    private bool[] _PlayerWaitingForGame;

    public void RegisterPlayer(Player plr) {
        if(_Players[(int)plr.PlayerNumber] != null) {
            Debug.LogError("Found two players with the same number!");
            return;
        }
        _Players[(int)plr.PlayerNumber] = plr;
    }

    public Player GetPlayerWithID(PlayerID id) {
        return _Players[(int)id];
    }

    public void LaunchMicrogame(PlayerID id) {
        _PlayerWaitingForGame[(int)id] = true;
        SceneManager.LoadSceneAsync("Beer Pong", LoadSceneMode.Additive);
        _Players[(int)id].UIAnimator.SetBool("microgame", true);
    }

    public void RegisterMicrogame(Microgame game) {
        for (int x = 0; x < _PlayerWaitingForGame.Length; x++) {
            if (!_PlayerWaitingForGame[x])
                continue;
            Debug.Assert(_LoadedMicrogames[x] == null);

            _PlayerWaitingForGame[x] = false;
            _LoadedMicrogames[x] = game;
            game.StartMicrogame(_Players[x]);
            return;
        }
    }

    public void DeregisterMicrogame(Microgame game) {
        for (int x = 0; x < _LoadedMicrogames.Length; x++) {
            if (_LoadedMicrogames[x] == game)
            {
                _Players[x].UIAnimator.SetBool("microgame", false);
                _LoadedMicrogames[x] = null;
                return;
            }
        }
    }

    private void Awake() {
        _Players = new Player[2];
        _LoadedMicrogames = new Microgame[2];
        _PlayerWaitingForGame = new bool[2];
    }

    private void OnEnable()
    {
        // singleton management
        if (_Instance != null && _Instance != this)
        {
            Debug.LogError("Multiple GameManagers detected! Killing one of them");
            Destroy(gameObject);
            return;
        }
        _Instance = this;
    }

    private void OnDisable()
    {
        // singleton management
        if (_Instance == this)
            _Instance = null;
    }
}