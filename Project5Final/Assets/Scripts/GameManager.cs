using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string gameState="Title Screen";
    public int playerLives;
    public int playerScore;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        // Game State Machine


        if (gameState == "Title Screen")
        {
            //Do behavior
            TitleScreen();
            //Check for Transitions

        }
        if (gameState == "Initialize Game")
        {
            //Do behavior
            InitializeGame();
            //Check for Transitions

        }
        if (gameState == "In Game")
        {
            //Do behavior
            InGame();
            //Check for Transitions

        }
        if (gameState == "Player Death")
        {
            //Do behavior
            PlayerDeath();
            //Check for Transitions

        }
        if (gameState == "Victory Screen")
        {
            //Do behavior
            VictoryScreen();
            //Check for Transitions

        }
        if (gameState == "Game Over Screen")
        {
            //Do behavior
            GameOverScreen();
            //Check for Transitions

        }
    }


    //Game State Functions
    private void TitleScreen()
    {

    }

    private void InitializeGame()
    {

    }

    private void InGame()
    {

    }

    private void PlayerDeath()
    {

    }

    private void VictoryScreen()
    {

    }

    private void GameOverScreen()
    {

    }
}
