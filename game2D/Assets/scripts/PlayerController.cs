using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Class variables
    public float speed;
    float xVel, yVel, sizeX, sizeY = 0;
    Animator anim;
    Rigidbody2D rb2d;
    public Image[] hearts;
    public int currentHealth;
    public int maxHealth;
    public GameObject sword, Bullet;
    //bool swordEquip, gunEquip;
    Vector3 camBindsMax, camBindsMin;
    public Vector2 maxSpeed;
    Vector3 playerPos;
    Quaternion playerRot;
    Transform playerTransform;





    void Start()
    {
        sizeX = GetComponent<BoxCollider2D>().bounds.size.x * 0.5f;
        sizeY = GetComponent<BoxCollider2D>().bounds.size.y * 0.5f;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        GetTran();
    }
    void GetTran()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos = playerTransform.position;
        playerRot = playerTransform.localRotation;
    }

    
    void Update()
    {
        
        Health();
        EquipWeapon();
        Death();
    }

    private void FixedUpdate()
    {
        GetTran();
        DoMove();
    }

    void EquipWeapon()
    {
     
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(sword, playerPos, playerRot);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(Bullet, playerPos, playerRot);
        }
    }

    void DoMove()
    {


        float horzMove, vertMove;

        if (Input.GetAxis("Horizontal") != 0)
        {
            horzMove = Input.GetAxis("Horizontal");
    
            if (horzMove > 0 && !InsideScreenMax() || horzMove < 0 && !InsideScreenMin())
            {
                float newXVel = Mathf.SmoothDamp(rb2d.velocity.x, 0, ref xVel, 0.06f);
                horzMove = 0;
                rb2d.velocity = new Vector2(newXVel, rb2d.velocity.y);
            }
            rb2d.AddForce(new Vector2(horzMove * speed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }
        
        else
        {
            float newXVel = Mathf.SmoothDamp(rb2d.velocity.x, 0, ref xVel, 0.06f); 
            horzMove = 0;
            rb2d.velocity = new Vector2(newXVel, rb2d.velocity.y);
        }

     
        if (Input.GetAxis("Vertical") != 0)
        {
            vertMove = Input.GetAxis("Vertical");
            if (vertMove > 0 && !InsideScreenMax() || vertMove < 0 && !InsideScreenMin())
            {
                float newYVel = Mathf.SmoothDamp(rb2d.velocity.y, 0, ref yVel, 0.06f);
                vertMove = 0;
                rb2d.velocity = new Vector2(rb2d.velocity.x, newYVel);
            }
            rb2d.AddForce(new Vector2(0, vertMove * speed * Time.deltaTime), ForceMode2D.Impulse);
        }
        
        else
        {
            float newYVel = Mathf.SmoothDamp(rb2d.velocity.y, 0, ref yVel, 0.06f);
            vertMove = 0;
            rb2d.velocity = new Vector2(rb2d.velocity.x, newYVel);
        }

        
        anim.SetFloat("HorzMove", horzMove);
        anim.SetFloat("VertMove", vertMove);

        
        bool Idle = (horzMove == 0 && vertMove == 0) ? true : false;
        anim.SetBool("Idle", Idle);
        
      
        ControlVelocity();


        BindPlayerToScreen();
    }

    void Health()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < currentHealth; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }

   

    public void GetDamaged()
    {
        currentHealth--;
    }

    void BindPlayerToScreen()
    {
        camBindsMax = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - sizeX, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - sizeY, 0);
        camBindsMin = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + sizeX, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + sizeY, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, camBindsMin.x + (sizeX * 1.3f), camBindsMax.x - (sizeX * 1.3f)), Mathf.Clamp(transform.position.y, camBindsMin.y + (sizeY * 1.3f), camBindsMax.y - (sizeY * 1.3f)), transform.position.z);
    }

    bool InsideScreenMax()
    {
        if (transform.position.x + sizeX >= camBindsMax.x || transform.position.y + sizeY >= camBindsMax.y) return false;
        else return true;
    }

    bool InsideScreenMin()
    {
        if (transform.position.x - sizeX <= camBindsMin.x || transform.position.y - sizeY <= camBindsMin.y) return false;
        else return true;
    }

    void ControlVelocity()
    {
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed.x)
        {
            float newSpeed = (rb2d.velocity.x > 0) ? maxSpeed.x : -maxSpeed.x;
            rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        }
        if (Mathf.Abs(rb2d.velocity.y) > maxSpeed.y)
        {
            float newSpeed = (rb2d.velocity.y > 0) ? maxSpeed.y : -maxSpeed.y;
            rb2d.velocity = new Vector2(rb2d.velocity.x, newSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetDamaged();
           
        }
    }
    void Death()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("Loss");
        }
    }
    
}
