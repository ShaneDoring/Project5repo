using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 5f;
    


    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {

        rigidBody.velocity = new Vector3(-bulletSpeed, 0, 0);
    }
}
