using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private Button retryButton;

    [SerializeField]
    private Button menuButton;

    [SerializeField]
    private GameManager gameManager;

    public void OnRetryButtonClick()
    {
        Debug.Log("Retry Button Clicked");
        gameManager.ChangeState(GameManager.GameState.Playing);
    }
    public void OnMenuButtonClick()
    {
        Debug.Log("Menu Button Clicked");
        gameManager.ChangeState(GameManager.GameState.Menu);
    }
}
