using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager ui;
    private bool gameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;

        Debug.Log("GAME OVER FUNCTION CALLED");

        Time.timeScale = 0f;
        ui.ShowGameOver();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void ResetGame()
    {
        gameOver = false;
    }
}
