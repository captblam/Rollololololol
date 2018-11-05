using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Required script components
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]

public class EnemyAI_FleeFire : MonoBehaviour {

 
    public TextMesh demoCharDynamicText;

   
    LineRenderer LR;

    Transform playerTransform;
    Vector3 playerPos, camBindsMax, camBindsMin, randomPos, targetPos;
    Rigidbody2D rb2D;
    float sizeX, sizeY;
    public float aggroDist, fleeDist, speed, acceptMoveToDistance;
    public Vector2 maxSpeed;
    public int enemHealth;
    private GameManager gamemanager;


    void Start () {
        randomPos = GetRandomPosition();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        LR = GetComponent<LineRenderer>();
        LR.startWidth = 0.1f;
        LR.endWidth = 0.3f;
        LR.SetPosition(0, transform.position);
        LR.SetPosition(1, transform.position);
        rb2D = GetComponent<Rigidbody2D>();
        sizeX = GetComponent<BoxCollider2D>().bounds.size.x * 0.5f;
        sizeY = GetComponent<BoxCollider2D>().bounds.size.y * 0.5f;
    }
	
	void Update () {
        playerPos = playerTransform.position;
	}

    private void FixedUpdate()
    {
        MoveTo();
    }

    void MoveTo()
    {
        randomPos = (Vector2.Distance(transform.position, randomPos) <= acceptMoveToDistance) ? GetRandomPosition() : randomPos;
       // demoCharDynamicText.text = "Mode: Guarding";
        LR.SetPosition(1, transform.position);
        if (Vector2.Distance(transform.position, playerPos) <= aggroDist)
        {
            //Player is inside aggro range

            if (Vector2.Distance(transform.position, playerPos) <= fleeDist)
            {
              
               // demoCharDynamicText.text = "Mode: Fleeing";
                rb2D.AddForce((transform.position - playerPos).normalized * speed, ForceMode2D.Impulse);
            }
            else
            {
                
                rb2D.velocity = Vector2.zero;
               // demoCharDynamicText.text = "Mode: Shooting";
                LR.SetPosition(0, transform.position);
                LR.SetPosition(1, playerPos);
            }
            
        }
        else if (Vector2.Distance(transform.position, playerPos) != aggroDist)
        {
            //demoCharDynamicText.text = "Mode: Wander";
            targetPos = EnemyWithinDistance() ? playerPos : randomPos;
            rb2D.AddForce(((targetPos - transform.position).normalized * speed), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Color aggCol = Color.red;
        aggCol.a = 0.1f;
        Gizmos.color = aggCol;
        Gizmos.DrawSphere(transform.position, aggroDist);
        aggCol = Color.yellow;
        aggCol.a = 0.1f;
        Gizmos.color = aggCol;
        Gizmos.DrawSphere(transform.position, fleeDist);
    }
    Vector3 GetRandomPosition()
    {
        camBindsMax = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - sizeX, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - sizeY, 0);
        camBindsMin = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + sizeX, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + sizeY, 0);
        Vector3 calcVec = new Vector3(Random.Range(camBindsMin.x, camBindsMax.x), Random.Range(camBindsMin.y, camBindsMax.y), transform.position.z);
        return calcVec;
    }
    bool EnemyWithinDistance()
    {
        if (Vector2.Distance(transform.position, playerPos) <= aggroDist)
        {
            return true;
        }
        else return false;
    }
    private void OnTriggerEnter2D(Collider2D isCol)
    {
        if (isCol.gameObject.tag == "sword")
        {
            GetDamaged();
        }
    }
    void GetDamaged()
    {
        enemHealth--;
        if (enemHealth <= 0)
        {
            Destroy(gameObject);
            
        }
    }
    

}
