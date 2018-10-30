using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObby : MonoBehaviour {
    bool Destroyable = false;
    Transform trans;
    public float speed;
    float newX;


    // Use this for initialization
    void Start()
    {
        float size = Random.Range(2, 3.4f);
        trans = GetComponent<Transform>();
        trans.localScale = new Vector3(size, size, 1);
        newX = trans.position.x;
        speed = Spawner.Instance.ObstacleSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        trans.position = new Vector3(newX -= speed * Time.deltaTime, trans.position.y, trans.position.z);
        newX = trans.position.x;
        transform.Rotate(0, 0, 50 * Time.deltaTime);
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
