using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public enum GameState
    {
        inGame,
        gameOver,
        loading,
        paused
    }

    private const string MAX_SCORE = "MAX_SCORE";

    public GameState gameState;
    
    public List<GameObject> targetPrefabs;

    [SerializeField] private float spawnRate = 1.5f;

    private int _score;
    
    private int Score
    {
        // The score can't be less than zero
        set => _score = Mathf.Max(value, 0);
        get => _score;
    }
    
    private int numberOfLives = 3;

    public List<GameObject> lives;

    // UI
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject titleScreen;
    
    //[SerializeField] private Button exitButton;

    private void Start()
    {
        ShowMaxScore();
    }

    /// <summary>
    /// Method that starts the game, changing the game status and starting the spawning coroutine
    /// </summary>
    /// <param name="difficulty">Integer number to indicate the game difficulty level</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;

        spawnRate /= difficulty;

        numberOfLives -= difficulty - 1;
        
        StartCoroutine(SpawnTarget());

        UpdateScore(0);
        
        gameOverText.gameObject.SetActive(false);
        
        titleScreen.gameObject.SetActive(false);
        
        scoreText.gameObject.SetActive(true);

        for (int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }
    }

    /// <summary>
    /// Spawns targets indefinitely
    /// </summary>
    /// <returns>-</returns>
    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);

            int idx = Random.Range(0, targetPrefabs.Count);

            Instantiate(targetPrefabs[idx]);
            
            //UpdateScore(5);
        }
    }

    /// <summary>
    /// Updates the game score and the display text
    /// </summary>
    /// <param name="scoreToAdd">Amount of points to add to the global score</param>
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score: " + Score;
    }

    /// <summary>
    /// Shows the max player score
    /// </summary>
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score: " + maxScore;
    }

    /// <summary>
    /// Sets the player max score in history
    /// </summary>
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);

        if (Score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, _score);
            
            // TODO: put an amazing particle system when the player reach a new record
        }
    }

    /// <summary>
    /// Checks the game over condition, decreases lives and shows the game over message
    /// </summary>
    public void GameOver()
    {
        numberOfLives--;

        Image heartImage = lives[numberOfLives].GetComponent<Image>();

        // Use var only for temp variables
        var tempColor = heartImage.color;

        tempColor.a = 0.3f;

        heartImage.color = tempColor;

        if (numberOfLives <= 0)
        {
            SetMaxScore();
            gameState = GameState.gameOver;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ShowMaxScore();
    }
}
