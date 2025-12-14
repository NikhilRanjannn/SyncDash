using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public TMP_Text scoreText;

    private int score;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        score = 0;
        scoreText.text = "0";

        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        score = 0;
        scoreText.text = "0";

        GameManager.Instance.ResetGame();

        Time.timeScale = 1f;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    // ADD SCORE FUNCTION
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    // TRY AGAIN
    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // EXIT GAME
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
