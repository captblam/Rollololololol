using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    bool Destroyable = false;
    Transform trans;
    public float speed;
    float newX;


	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        trans.localScale = new Vector3(1, Random.Range(2,3.4f), 1);
        newX = trans.position.x;
        speed = Spawner.Instance.ObstacleSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        trans.position = new Vector3(newX -= speed * Time.deltaTime, trans.position.y, trans.position.z);
        newX = trans.position.x;
	}

    private void OnDestroy()
    {
        Spawner.Instance.NumSpawned--;
    }

    private void OnBecameInvisible()
    {
        if (Destroyable)
        Destroy(gameObject);
    }

    private void OnBecameVisible()
    {
        Destroyable = true;
    }
}
