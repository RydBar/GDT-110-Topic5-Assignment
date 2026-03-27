using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Canvas mainMenuCanvas;
    [SerializeField]
    private Canvas playingCanvas;
    [SerializeField]
    private Canvas gameOverCanvas;
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

        playingCanvas.enabled = false;
        gameOverCanvas.enabled = false;
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

    private void TriggerGameStart()
    {
        Debug.Log("GameStart triggered");
        playerObject.transform.position = Vector3.zero;
        playerObject.SetActive(true);
        player.enabled = true;
        mainMenuCanvas.enabled = false;
        playingCanvas.enabled = true;
    }
    private void TriggerGameOver()
    {
        Debug.Log("GameOver triggered");
        player.enabled = false;
        playerObject.SetActive(false);
        gameOverCanvas.enabled = true;
    }

    private void TriggerMenu()
    {
        Debug.Log("Menu triggered");
        gameOverCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }
}
