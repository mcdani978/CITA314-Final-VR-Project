using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score Settings")]
    private int currentScore = 0;
    public int totalPins = 10;
    public int winScore = 100;

    public TextMeshProUGUI scoreText;

    [Header("UI Elements")]
    public GameObject menuCanvas;        
    public Button startButton;
    public Button restartButton;
    public GameObject scoreUI;         

    private bool hasGameStarted = false;

    [Header("Audio")]
    public AudioClip buttonClick;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 0f; // Pause at the start
        menuCanvas.SetActive(true);              // Show start menu
        restartButton.gameObject.SetActive(false); // Hide restart at first
        scoreUI.SetActive(false);                // Hide score UI

        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        UpdateScoreText();

        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        if (audioSource && buttonClick)
            audioSource.PlayOneShot(buttonClick);

        hasGameStarted = true;
        Time.timeScale = 1f; 
        menuCanvas.SetActive(false);      
        scoreUI.SetActive(true);          
    }

    public void EndGame()
    {
        hasGameStarted = false;
        Time.timeScale = 0f;             
        menuCanvas.SetActive(true);       
        startButton.gameObject.SetActive(false);   
        restartButton.gameObject.SetActive(true); 
        scoreUI.SetActive(false);      
    }

    public void RestartGame()
    {
        if (audioSource && buttonClick)
            audioSource.PlayOneShot(buttonClick);

        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();

        if (currentScore >= winScore)
        {
            Invoke("EndGame", 2f);  
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    public bool HasGameStarted()
    {
        return hasGameStarted;
    }
}
