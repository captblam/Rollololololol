using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGetter : MonoBehaviour {
    public float score = 1;
    public float scoreMultiplier = 1.0f;
    public  float CurrentScore;
    public Text Score;
    public Text ScoreMult;
    public GameObject Limelight;
    bool isCollide = false;
  
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CurrentScore = score += Time.deltaTime * scoreMultiplier;
        Score.text = "Current Score: " + CurrentScore + " Points";
        ScoreMult.text = "Multiplier: " + scoreMultiplier + " x";
        if(isCollide == true)
        {
            scoreMultiplier++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollide = false;
        }
    }
   
}

