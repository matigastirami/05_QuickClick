﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public enum GameState
    {
        inGame,
        gameOver,
        loading,
        paused
    }

    public GameState gameState;
    
    public List<GameObject> targetPrefabs;

    [SerializeField] private float spawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int _score;
    
    private int Score
    {
        // The score can't be less than zero
        set => _score = Mathf.Max(value, 0);
        get => _score;
    }

    [SerializeField] private TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.inGame;
        
        StartCoroutine(SpawnTarget());

        UpdateScore(0);
        
        gameOverText.gameObject.SetActive(false);
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
    /// Shows the game over message
    /// </summary>
    public void GameOver()
    {
        gameState = GameState.gameOver;
        gameOverText.gameObject.SetActive(true);
    }
}
