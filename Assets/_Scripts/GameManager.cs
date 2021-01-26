using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TextMeshProUGUI;

public class GameManager : MonoBehaviour
{
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
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());

        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (true)
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
}
