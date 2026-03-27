using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private GameManager gameManager;

    public void OnPlayButtonClick()
    {
        Debug.Log("Play Button Clicked");
        gameManager.ChangeState(GameManager.GameState.Playing);
    }
    public void OnQuitButtonClick()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
}
