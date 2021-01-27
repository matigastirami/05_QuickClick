using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button _button;
    
    private GameManager gameManager;

    [SerializeField]
    [Range(1,3)]
    private int difficulty;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
