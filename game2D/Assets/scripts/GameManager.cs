using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] Enemies;
    public Vector3[] spawnPositions;
    public Quaternion spawnRotation;
    public int enemNum, WaveNum;
    public float enemWait, enemStart, WaveSpawn;
    

    private void Start()
    {
        StartCoroutine (Spawner());
    }
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(enemStart);
        while(WaveNum <= 5)
        {   for (int i = 0; i < enemNum; i++)
            {
                
                Instantiate(Enemies[Random.Range(0, 3)], spawnPositions[Random.Range(0, 2)], spawnRotation);
                yield return new WaitForSeconds(enemWait);
            }
            WaveReset();
            WaveNum++;
            yield return new WaitForSeconds(WaveSpawn);
        }
    }
    void WaveReset()
    {
        if (WaveNum >= 5)
        {
            WaveNum = 0;
        }
    }


}

