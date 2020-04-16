using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    ScoreUI scoreUI;
    ScoreHandler scoreHandler = new ScoreHandler();
    LevelManager levelManager;

    private void Awake() {
        scoreUI = FindObjectOfType<ScoreUI>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void UpdateScore(int numberOfBlocks){
        int score = scoreHandler.Calculate(numberOfBlocks);
        UpdateUI(score);
    }

    public void AddScoreForMoves(int moves){
        int score = scoreHandler.CalculateMovesScore(moves);
        UpdateUI(score);
    }

    public void UpdateUI(int score){
        score = levelManager.AddScore(score);
        scoreUI.UpdateScore(score);
    }
}
