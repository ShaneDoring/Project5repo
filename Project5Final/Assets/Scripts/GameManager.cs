using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string gameState="Title Screen"; //Holds the current game state that will run during update method
    public int playerLives=3;
    public int playerScore=0;
    public int currentSceneIndex = 0;
    

    public GameObject playerPrefab;
    public GameObject player;
    public GameObject playerSpawnPoint;
    public GameObject playerDeathScreen;

    public Vector3 playerRespawnPoint;


    


    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {    //Creates instance of Game Manager Object
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //Prevents Game manager from being destoyed on scene changes
        }
        else
        {
            //error checking to prevent second game manager being loaded
            Debug.LogError("Game manager tried to load in a second game manager");
            Destroy(this);
        }

    }

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
                //if player presses any key, will transition to Initialize Game gameState
                //if player presses escape game will quit

        }
       else if (gameState == "Initialize Game")
        {
            //Do behavior
            InitializeGame(); //loads first Level
            //Check for Transitions
            
            
                ChangeState("Spawn Player");
            
                       

        }
        else if(gameState=="Spawn Player")
        {
            //do behavior
            SpawnPlayer();
            //check for transitions
            
            if (GameManager.instance.player != null)
            {
                ChangeState("In Game");
            }
        }
        else if (gameState == "In Game")
        {
            //Do behavior
            InGame();
            //Check for Transitions
            if (GameManager.instance.player == null)
            {
                ChangeState("Player Death");
            }


        }
        else if (gameState == "Player Death")
        {
            //Do behavior
            PlayerDeath();
            //Check for Transitions
            ChangeState("Game Over Screen");
        }
        else if (gameState == "Victory Screen")
        {
            //Do behavior
            VictoryScreen();
            //Check for Transitions

        }
        else if (gameState == "Game Over Screen")
        {
            //Do behavior
            GameOverScreen();
            //Check for Transitions

        }
    }


    //Game State Functions
    private void TitleScreen()
    {
        //Wait for player input
        PlayerChoiceExitOrStartGame();

    }

    private void InitializeGame()
    {
        LoadLevel(1);
    }
    public void SpawnPlayer()//Creates Instance of Playable Character at the designated Spawn point
    { 
        if(player==null)
        {
            player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
        }
    }
    private void InGame()
    {

    }

    private void PlayerDeath()
    {
        //reduce player lives remaining
        playerLives -= 1;
        //Show the Death Scene
        LoadLevel(5);
    }

    private void RespawnPlayer()
    {

    }

    private void VictoryScreen()
    {

    }

    private void GameOverScreen()
    {
        //wait for player input
        PlayerChoiceExitOrStartGame();
    }
    public void ChangeState(string newState)
    {
        gameState = newState;
    }

    // Scene Functions

    //Overloaded function for scene loading with name or scene index number
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.LogError("Scene finished loading..");
        currentSceneIndex = scene.buildIndex;
    }

    public void LoadNextScene()
    {
        LoadLevel(currentSceneIndex + 1);
    }

    public void PlayerChoiceExitOrStartGame()
    {
        if (Input.anyKey)
        {
            ChangeState("Initialize Game");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
