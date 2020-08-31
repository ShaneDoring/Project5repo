using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesUI : MonoBehaviour
{

    public static int livesRemaining = 0;
    Text lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesRemaining = GameManager.instance.playerLives;
        lives.text = "Lives: " + livesRemaining;
    }
}
