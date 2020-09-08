using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {

        if (otherObject.gameObject.CompareTag("Player"))
        {
            GameManager.instance.LoadNextScene();
        }
    }
}
