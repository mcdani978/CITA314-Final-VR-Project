using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentScore = 0;
    public int totalPins = 10; 
    public int winScore = 10; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log("Score: " + currentScore);

        if (currentScore >= winScore)
        {
            Invoke("ResetGame", 2f); // Wait 2 seconds before resetting
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

