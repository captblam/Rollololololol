using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScripter : MonoBehaviour {
    public GameObject Obj;
    public float liveTime;
	// Use this for initialization
	void Start () {
        Ded();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Ded()
    {
        Destroy(Obj, liveTime);
    }
}
