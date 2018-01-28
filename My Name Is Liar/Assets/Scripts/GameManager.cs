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
    private string[] _LoadingMicrogames;
    private GameObject[] npcs;

    public static readonly string[] MicrogameNames = {
        //"Beer Pong", "Dance Off", "Lizard", "Dance", "Pushups"
		"Pushups"
    };

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

    public GameObject GetNPCForPlayer(PlayerID id) {
        return npcs[(int)id];
    }


	public void LaunchMicrogame(PlayerID id, GameObject npc) {
        if(PlayingMicrogame(id))
            return;

        // randomly pick a microgame
        int r = Random.Range(0, MicrogameNames.Length);
        string scene_name = MicrogameNames[r];

        SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        _Players[(int)id].UIAnimator.SetTrigger("start");
        _LoadingMicrogames[(int)id] = scene_name;
			
		npcs [(int)id] = npc;
    }

    public bool PlayingMicrogame(PlayerID id) {
        return _LoadedMicrogames[(int)id] != null;
    }

    public Microgame GetMicrogameForPlayer(PlayerID id) {
        return _LoadedMicrogames[(int)id];
    }

    public void RegisterMicrogame(Microgame game) {
        var name = game.gameObject.scene.name;

        for (int x = 0; x < _LoadingMicrogames.Length; x++) {
            if (!name.Equals(_LoadingMicrogames[x]))
                continue;
            Debug.Assert(_LoadedMicrogames[x] == null);

            _LoadingMicrogames[x] = null;
            _LoadedMicrogames[x] = game;
            game.StartMicrogame(_Players[x]);
            return;
        }
    }

    public void DeregisterMicrogame(Microgame game, bool won) {
        for (int x = 0; x < _LoadedMicrogames.Length; x++) {
            if (_LoadedMicrogames[x] == game)
            {
                if(won)
                    _Players[x].UIAnimator.SetTrigger("win");
                else
                    _Players[x].UIAnimator.SetTrigger("lose");
                npcs[x] = null;
                _LoadedMicrogames[x] = null;
                return;
            }
        }
    }

    private void Awake() {
        _Players = new Player[2];
        _LoadedMicrogames = new Microgame[2];
        _LoadingMicrogames = new string[2];
        npcs = new GameObject[2];
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