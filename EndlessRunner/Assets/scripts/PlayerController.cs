﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Starting Variables
    Rigidbody2D rb;
    [SerializeField]bool grounded = true;
    public float JumpHeight;
    public bool isAlive;
    

	void Start () {

        //Getting and initializing starting variables
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {

        //checking for player input every tick
		if (Input.GetAxis("Jump") > 0 && grounded)
        {
            grounded = false;
            rb.AddForce(new Vector2(0, JumpHeight * Input.GetAxis("Jump")), ForceMode2D.Impulse);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to see if we hit the floor (the only thing we care about to check for grounded boolean)
        if (collision.gameObject.layer == 8)
        {
            grounded = true;
        }
    }

    private void OnBecameInvisible()
    {
        // I CAN'T BE SEEN. (show game over screen, which includes button to start over)
        isAlive = false;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            rb.AddForce(new Vector2(0, JumpHeight * 1.2f), ForceMode2D.Impulse);
        }
       
    }
   
    public void IsDead()
    {
        if (isAlive == false)
        {

            SceneManager.LoadScene("LossScene");
        }
    }
        

}
