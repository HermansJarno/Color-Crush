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
        score = levelManager.AddScore(score);
        scoreUI.UpdateScore(score);
    }
}
