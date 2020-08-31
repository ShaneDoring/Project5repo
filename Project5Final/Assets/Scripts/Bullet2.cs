using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 5f;
    public int damage = 1;
    public Rigidbody2D rigidBody;
    public int secondsToDestroy = 4;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {

        rigidBody.velocity = new Vector3(-bulletSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5);
        }

    }
}
