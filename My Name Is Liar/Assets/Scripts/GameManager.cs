using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake() {
        _Players = new Player[2];
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