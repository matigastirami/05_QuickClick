using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] private float spawnRate = 1.5f;

    private int _score;
    
    private int Score
    {
        // The score can't be less than zero
        set => _score = Mathf.Max(value, 0);
        get => _score;
    }

    // UI
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject titleScreen;
    
    //[SerializeField] private Button exitButton;

    /// <summary>
    /// Method that starts the game, changing the game status and starting the spawning coroutine
    /// </summary>
    /// <param name="difficulty">Integer number to indicate the game difficulty level</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;

        spawnRate /= difficulty;
        
        StartCoroutine(SpawnTarget());

        UpdateScore(0);
        
        gameOverText.gameObject.SetActive(false);
        
        titleScreen.gameObject.SetActive(false);
        
        scoreText.gameObject.SetActive(true);
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
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
