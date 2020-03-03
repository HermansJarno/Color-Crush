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

    private void Awake() {
        levelUI = FindObjectOfType<LevelUI>();
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
        levelUI.UpdateUI();
    }

    public int Moves {
        get {
            return moves;
        }
    }
}
