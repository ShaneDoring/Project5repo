using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour
{
    public int health = 1;
    public float moveSpeed;
    public float aiSeeDistance=3;
    public bool moveRight;
    private float timeBetweenAttack;//Delay between attack cycles
    public float startTimeToAttack;//time attack is ready
    public Transform firePoint;
    public Transform castPoint;
    private SpriteRenderer sprite;
    private Animator animator;
    public string enemyState = "Patrol";


    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;


    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        animator.SetTrigger("Shoot Player");
    }

    // Update is called once per frame
    void Update()
    {
        //State Machine for Enemy AI

        if (enemyState == "Patrol")
        {
            //do behavior
            Patrol();
            //check for transition
            if (CanSee(aiSeeDistance))
            {
                ChangeEnemyState("Ready Shot");
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
       
        }

        if (enemyState == "Ready Shot")
        {
            //do behavior
            ReadyShot();
            //check for transitions
            if (CanSee(aiSeeDistance)==false)
            {
                ChangeEnemyState("Patrol");
            }
            ChangeEnemyState("Shoot Player");

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (enemyState == "Shoot Player")
        {
            //do behavior
            if (timeBetweenAttack <= 0)
            {
                ShootPlayer();
                timeBetweenAttack = startTimeToAttack;
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }
          
            //check for transitions
            if (CanSee(aiSeeDistance) == false)
            {
                ChangeEnemyState("Patrol");
            }
            if (health <= 0)
            {
                ScoreScript.scoreValue += 100;
                Destroy(gameObject);
            }
        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken!");
    }

    //Enemy patrol behaviour
    private void Patrol()
    {
        animator.SetBool("isMoving", true);
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
            sprite.flipX = false;
            
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
            sprite.flipX = true;
            
        }
       
    }

    //Enemy Seek Behavior
    private void ReadyShot()
    {
        animator.SetBool("isMoving", false);
    }

    //shoot player functions
    private void ShootPlayer()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Shoot");
        if (moveRight == true)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);          
        }
        if (moveRight == false)
        {
            Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        }
        
    }

    private void ChangeEnemyState(string newEnemyState)
    {
        enemyState = newEnemyState;
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Turn"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
                moveRight = true;
        }
    }

    private bool CanSee(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (moveRight == false)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position+Vector3.right*castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hit.point,Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return val;
     }
   
}
