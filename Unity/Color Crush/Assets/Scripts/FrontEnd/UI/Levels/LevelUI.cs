using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private LevelManager levelManager;
    public Text movesTextField;

    private void Awake() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void UpdateUI(){
        UpdateMoves();
    }

    private void UpdateMoves(){
        movesTextField.text = levelManager.Moves.ToString();
    }
}
