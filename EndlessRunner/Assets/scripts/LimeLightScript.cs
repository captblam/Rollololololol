using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeLightScript : MonoBehaviour
{

    bool isRight = true;
    public float speed = 0.5f;

    void Update()
    {
        if (isRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 5.0f)
        {
            isRight = false;
        }

        if (transform.position.x <= -12.0f)
        {
            isRight = true;
        }
    }
}

