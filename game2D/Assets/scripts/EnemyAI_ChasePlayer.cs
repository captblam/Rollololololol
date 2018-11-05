using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]

public class EnemyAI_ChasePlayer : MonoBehaviour
{
    public TextMesh demoCharDynamicText;
    Transform playerTransform;
    Vector3 playerPos, targetPos, randomPos;
    Rigidbody2D rb2D;
    Vector3 camBindsMax, camBindsMin;
    public Vector2 maxSpeed;
    public float aggroDistance, acceptMoveToDistance, speed;
    float sizeX, sizeY, aggroSpeed, normSpeed;
    public int enemHealth;
    private GameManager gamemanager;

    // Use this for initialization
    void Start()
    {
        normSpeed = (maxSpeed.x + maxSpeed.y) / 4;
        aggroSpeed = normSpeed * 1.2f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos = playerTransform.position;
        randomPos = GetRandomPosition();
        rb2D = GetComponent<Rigidbody2D>();
        sizeX = GetComponent<BoxCollider2D>().bounds.size.x * 0.5f;
        sizeY = GetComponent<BoxCollider2D>().bounds.size.y * 0.5f;
    }

    void Update()
    {

     
        float Dist = Vector2.Distance(transform.position, playerTransform.position);

      
       // demoCharDynamicText.text = (Dist > aggroDistance) ? "Mode: Wander" : "Mode: Chase";
       // demoCharDynamicText.color = (Dist > aggroDistance) ? Color.green : Color.red;
        
       
        speed = (Dist > aggroDistance) ? normSpeed : aggroSpeed;

 
        playerPos = playerTransform.position;
    }

    void FixedUpdate()
    {
        Move();
        ControlVelocity();
    }

    void Move()
    {
        randomPos = (Vector2.Distance(transform.position, randomPos) <= acceptMoveToDistance) ? GetRandomPosition() : randomPos;
        targetPos = EnemyWithinDistance() ? playerPos : randomPos;
        rb2D.AddForce(((targetPos - transform.position).normalized * speed), ForceMode2D.Impulse);
    }

    bool EnemyWithinDistance()
    {
        if (Vector2.Distance(transform.position, playerPos) <= aggroDistance)
        {
            return true;
        }
        else return false;
    }

    Vector3 GetRandomPosition()
    {
        camBindsMax = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - sizeX, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - sizeY, 0);
        camBindsMin = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + sizeX, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + sizeY, 0);
        Vector3 calcVec = new Vector3(Random.Range(camBindsMin.x, camBindsMax.x), Random.Range(camBindsMin.y, camBindsMax.y), transform.position.z);
        return calcVec;
    }

    void ControlVelocity()
    {
        if (Mathf.Abs(rb2D.velocity.x) > maxSpeed.x)
        {
            float newSpeed = (rb2D.velocity.x > 0) ? maxSpeed.x : -maxSpeed.x;
            rb2D.velocity = new Vector2(newSpeed, rb2D.velocity.y);
        }
        if (Mathf.Abs(rb2D.velocity.y) > maxSpeed.y)
        {
            float newSpeed = (rb2D.velocity.y > 0) ? maxSpeed.y : -maxSpeed.y;
            rb2D.velocity = new Vector2(rb2D.velocity.x, newSpeed);
        }
    }

  
    void OnDrawGizmos()
    {
        Color gizColor = Color.red;
        gizColor.a = 0.1f;
        Gizmos.color = gizColor;
        Gizmos.DrawSphere(transform.position, aggroDistance);
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
