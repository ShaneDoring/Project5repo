using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite redCheckpoint;
    public Sprite greenCheckpoint;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;

    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            checkpointSpriteRenderer.sprite = greenCheckpoint;
            checkpointReached = true;
        }
    }
}
