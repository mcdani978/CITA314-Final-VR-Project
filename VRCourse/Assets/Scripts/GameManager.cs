using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentScore = 0;
    public int totalPins = 10;
    public int winScore = 100;

    public TextMeshProUGUI scoreText; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();

        if (currentScore >= winScore)
        {
            Invoke("ResetGame", 2f);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

