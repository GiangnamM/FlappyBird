using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{

    public static GamePlayController instance;

    [SerializeField]
    private Button instructionButton;

    [SerializeField] private Text scoreText, endScoreText, bestScoreText;

    [SerializeField] private GameObject gameOverPanel, pausePanel;
    private void Awake()
    {
        Time.timeScale = 0;
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    public void InstructionButton()
    {
        Time.timeScale = 1;
        instructionButton.gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void BirdDiedShowPanel(int score)
    {
        gameOverPanel.SetActive(true);
        endScoreText.text = "" + score;
        if (score > GameManager.instance.GetHighScore())
        {
            GameManager.instance.SetHighScore(score);
        }
        bestScoreText.text = "" + GameManager.instance.GetHighScore();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGameButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
