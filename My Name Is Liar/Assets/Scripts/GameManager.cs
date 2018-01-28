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

    public float TimeElapsed {
        get {
            return Time.time - _StartTime;
        }
    }

    public float TimeRemaining {
        get {
            return Mathf.Max(0, _GameLength - TimeElapsed);
        }
    }

    private float _StartTime;
    private const float _GameLength = 60 * 5;

    private List<NPC> _NPCList;

    public static readonly string[] MicrogameNames = {
		"Beer Pong", "Dance Off", "Lizard", "Dance", "Pushups", "GIRLS"
    };

    private void Start() {
        _StartTime = Time.time;
    }

    private bool _GameOver = false;

    private void Update() {
        if (TimeRemaining <= 0 && !_GameOver) {
            _GameOver = true;
            SceneManager.LoadScene("Winscreen");
        }
    }

    public PlayerID WinningPlayer() {
        int p1 = 0;
        int p2 = 0;
        foreach(var npc in _NPCList) {
            if (npc.opinion1 > npc.opinion2)
                p1++;
            else if (npc.opinion1 < npc.opinion2)
                p2++;
        }
        if (p1 > p2)
            return PlayerID.One;
        return PlayerID.Two;
    }

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

        var aso = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        aso.completed += (obj) => _Players[(int)id].UIAnimator.SetBool("loaded", true);
        _Players[(int)id].UIAnimator.SetTrigger("start");
        _LoadingMicrogames[(int)id] = scene_name;
			
		npcs [(int)id] = npc;
    }

    public bool PlayingMicrogame(PlayerID id) {
        return _LoadedMicrogames[(int)id] != null || _LoadingMicrogames[(int)id] != null;
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

    public NPC GetClosestNPCToPoint(Vector2 pos, out float dist) {
        float mindist = -1;
        NPC minnpc = null;
        foreach(var npc in _NPCList) {
            float sqdist = Vector2.SqrMagnitude(pos - (Vector2)npc.transform.position);
            if(sqdist < mindist || minnpc == null) {
                mindist = sqdist;
                minnpc = npc;
            }
        }
        dist = mindist < 0 ? 0 : Mathf.Sqrt(mindist);
        return minnpc;
    }

    public void RegisterNPC(NPC npc) {
        _NPCList.Add(npc);
    }

    public bool DeregisterNPC(NPC npc) {
        return _NPCList.Remove(npc);
    }

    private void Awake() {
        _Players = new Player[2];
        _LoadedMicrogames = new Microgame[2];
        _LoadingMicrogames = new string[2];
        npcs = new GameObject[2];
        _NPCList = new List<NPC>();
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