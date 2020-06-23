using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int moves = 0;
    [SerializeField] private int scoreGoal = 0;
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int score = 0;

    private LevelUI levelUI;
    private Level level;

    private LevelLoader levelLoader = new LevelLoader();

    private void Awake() {
        levelUI = FindObjectOfType<LevelUI>();
        currentLevel = 1;
        level = levelLoader.LoadLevel(currentLevel);

        foreach(var goal in level.LevelGoals){
            Debug.Log("load key: " + goal.Key + " with value: " + goal.Value);
        }

        Debug.Log(level.ColorGoals.Count);

        SetMoves(level.NumberOfMoves);
    }

    public int AddScore(int score){
        this.score += score;
        return this.score;
    }

    public void LowerMoves(){
        moves--;
        SetMoves(moves);
    }

    public void SetMoves(int moves){
        this.moves = moves;
        if(moves > 0){
            levelUI.UpdateUI();
        } else {
            GameObject.FindObjectOfType<LifesController>();
        }
    }

    public int Moves {
        get {
            return moves;
        }
    }
}
