using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner Instance = null;
    public GameObject[] Obstacles;
    public int MaxObjectsOnScreen;
    public int Ball = 0;
    public float Frequency;
    public Vector3 StartPOS;
    public float ObstacleSpeed;

    private float TimeOut;
    public int NumSpawned;


	// Use this for initialization
	void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        TimeOut = Frequency;
	}
	
	// Update is called once per frame
	void Update () {
        if (TimeOut < 0 && NumSpawned < MaxObjectsOnScreen)
        {
            GameObject obs = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], StartPOS, Quaternion.identity);
            TimeOut = Frequency;
            NumSpawned++;
        }
        else TimeOut -= Time.deltaTime;
	}
}
