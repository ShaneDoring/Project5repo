using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour
{
    public int health=1;
    public float moveSpeed;
    public float aiSense;
    public bool moveRight;

    private SpriteRenderer sprite;
    private Animator animator;
    public string enemyState = "Patrol";
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
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (enemyState == "Shoot Player")
        {
            //do behavior
            ShootPlayer();
            //check for transitions
            if (health <= 0)
            {
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

}
