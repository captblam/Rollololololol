using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedGetter : MonoBehaviour {

    public Text text;
    public GameObject player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update () {
        text.text = "Current Speed: " + rb.velocity + " km/h";
	}
}
