using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBetweenAttack;//Delay between attack cycles
    public float startTimeToAttack;//time attack is ready

    public Transform attackPosition;//The position the attack will originate
    public float attackRange;//the range of the attack from origin
    public LayerMask whatIsEnemies;//layermask for attacks to only attack enemies layer
    public int damage;//amount of damage done

    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //creates a circle collider and uses layermask to detect the amount of enemies within range, pressing left control initializes attack and plays attack animation and deals damage to enemy
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerAnim.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position,attackRange,whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemySoldier>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeToAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
               
    }

    //creates a gizmo to help developer see area of effet for attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
