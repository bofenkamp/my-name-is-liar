using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Navi : MonoBehaviour {

    public static Navi Instance {
        get { return _Instance; }
    }
    private static Navi _Instance;

    private Animator _Animator;

    public GameObject Following;

    private void Start() {
        _Animator = GetComponent<Animator>();
    }
	
	private void Update () {
        _Animator.SetBool("enabled", Following != null);
        if (Following != null)
            transform.position = Following.transform.position;
	}
}
