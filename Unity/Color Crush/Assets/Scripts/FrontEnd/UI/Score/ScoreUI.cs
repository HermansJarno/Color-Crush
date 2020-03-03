using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreTextField;

    public void UpdateScore(int score) {
        scoreTextField.text = score.ToString();
    }
}
