using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed=1f;
    
  

    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody.velocity = new Vector3(bulletSpeed, 0,0);
    }

    
   
}
