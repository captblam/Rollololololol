using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public GameObject obj;
    public float Livetime;
    // Use this for initialization
    void Start () {
        Ded();
    }
    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        Shot();
    }
    void Shot()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
    void Ded()
    {
        Destroy(obj, Livetime);
    }
}
