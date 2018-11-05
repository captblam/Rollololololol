using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemController : MonoBehaviour {

    public int enemHealth;
    public float speed;
    private GameManager gamemanager;
 
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D isCol)
    {
        if(isCol.gameObject.tag == "sword")
        {
            enemHealth--;
            if (enemHealth <= 0)
            {
                Destroy(gameObject);
               
            }
        }
    }
    
}
