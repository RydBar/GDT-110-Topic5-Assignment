using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    //Player script & Player Object
    [SerializeField]
    private Player player;
    [SerializeField]
    private int score;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]

    //Canvases & Text
    private Canvas mainMenuCanvas;
    [SerializeField]
    private Canvas playingCanvas;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Canvas gameOverCanvas;

    //Enemy Spawners
    [SerializeField]
    private GameObject enemySpawner1;
    [SerializeField]
    private GameObject enemySpawner2;
    public enum GameState
    {
        /// <summary>
        /// The menu state upon starting the game.
        /// </summary>
        Menu,

        /// <summary>
        /// The Playing state active during gameplay.
        /// </summary>
        Playing,

        /// <summary>
        /// GameOver gets switched to when the player dies.
        /// </summary>
        GameOver
    }

    public GameState currentState { get; private set; } = GameState.Menu;
    void Start()
    {

        playingCanvas.enabled = false;
        gameOverCanvas.enabled = false;

        if (player != null)
        {
            player.enabled = false;
        }
        else
        {
            player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                Debug.Log("No player assigned, player found");
                player.enabled = false;
            }
            else
            {
                Debug.Log("No player assigned or found!");
            }
        }

        if (playerObject != null)
        {
            playerObject.SetActive(false);
        }
        else
        {
            Debug.Log("PLAYER GAMEOBJECT NOT ASSIGNED!!!");
        }
    }

    public void ChangeState(GameState state)
    {
        switch (state)
        {
            case (GameState.Menu):
                TriggerMenu();
                break;
            case (GameState.Playing):
                TriggerGameStart();
                break;
            case (GameState.GameOver):
                TriggerGameOver();
                break;
        }
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    private void TriggerGameStart()
    {
        Debug.Log("GameStart triggered");
        playerObject.transform.position = Vector3.zero;
        playerObject.SetActive(true);
        player.enabled = true;
        mainMenuCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        playingCanvas.enabled = true;
        player.resetHealth();

        score = 0;

        enemySpawner1.SetActive(true);
        enemySpawner2.SetActive(true);
    }
    private void TriggerGameOver()
    {
        Debug.Log("GameOver triggered");
        player.enabled = false;
        playerObject.SetActive(false);
        gameOverCanvas.enabled = true;

        enemySpawner1.SetActive(false);
        enemySpawner2.SetActive(false);
    }

    private void TriggerMenu()
    {
        Debug.Log("Menu triggered");
        gameOverCanvas.enabled = false;
        mainMenuCanvas.enabled = true;

        score = 0;

        enemySpawner1.SetActive(false);
        enemySpawner2.SetActive(false);
    }
}
