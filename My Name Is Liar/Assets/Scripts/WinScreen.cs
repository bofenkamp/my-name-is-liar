using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

    [SerializeField]
    private string _MainMenuName;
    [SerializeField]
    private GameObject[] P1Objects;
    [SerializeField]
    private GameObject[] P2Objects;

	void Start () {
        bool p1win = GameManager.Instance.WinningPlayer() == PlayerID.One;

        foreach (var o in P1Objects)
            o.SetActive(p1win);
        foreach (var o in P2Objects)
            o.SetActive(!p1win);

        SceneManager.LoadSceneAsync("Map", LoadSceneMode.Additive);
	}

    public void DeleteAll()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
    }
	
	private void Update () {
        if(Input.anyKeyDown) {
            DeleteAll();
            SceneManager.LoadScene(_MainMenuName);
        }
	}
}
