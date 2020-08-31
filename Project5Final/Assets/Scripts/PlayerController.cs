using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed=2;
    public float jumpForce = 7;
    public int maxJumps = 2;
    public int currentJumps;
    public float jumpHeight = 1.1f;
    public int playerhealth=3;
    public int numOfHearts = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject deathAnimation;

    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Get all the Components for Player Character
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerhealth > numOfHearts)
        {
            playerhealth = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerhealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        //Check for player input for moevement left and right
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        animator.SetFloat("xMove", Mathf.Abs(xMovement));
        rigidBody.velocity = new Vector2(xMovement, rigidBody.velocity.y);

        //Check to flip player sprite when changing direcctions
        if (rigidBody.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        if (rigidBody.velocity.x < 0)
        {
            sprite.flipX = true;
        }

        //Check for player input for jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                currentJumps = maxJumps;
            }
            if (currentJumps > 0)
            {
                Jump();
            }
        }
    }

    //Player controlled Fucntions
    void Jump()
    {
        currentJumps--;
        animator.SetTrigger("Jump");
        animator.SetBool("Jump1", true);    
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    void Shoot()
    {

    }

    //Player Passive Functions

        //Checks whwether the player is touching the ground
    private bool IsGrounded()
    {
        float extraHeighttext = .1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeighttext);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeighttext), rayColor);
        Debug.Log(raycastHit.collider);
        bool grounded = raycastHit.collider != null;
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("Jump1", false);
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IsGrounded();
    }

    public void TakeDamage(int damage)
    {
        playerhealth -= damage;
        if (playerhealth <= 0)
        {
            //player dies
            Die();
        }

    }

    public void Die()
    {
        Instantiate(deathAnimation, transform.position, transform.rotation);
        Destroy(gameObject,.03f);
    }

}
