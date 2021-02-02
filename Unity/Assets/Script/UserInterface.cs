using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Button closeButton;
    public Text scoreText;
    public Text finalScoreText;
    public GameObject gameOverPanel;
    public GameObject gamePlayPanel;
    public Button restartButton;
    private AppCoolerman app;

    public void Close()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Start()
    {
        app = FindObjectOfType<AppCoolerman>();
    }

    public void Update()
    {
        scoreText.text = app.score.ToString();
    }

    internal void GameOver()
    {
        finalScoreText.text = scoreText.text;
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        restartButton.onClick.AddListener(Restart);
        closeButton.onClick.AddListener(Close);
    }
}