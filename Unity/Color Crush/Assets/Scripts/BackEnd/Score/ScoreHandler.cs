using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler
{
    int scoreBase = 100;

    public int Calculate(int numberOfBlocks){
        return numberOfBlocks;
    }

    public int CalculateMovesScore(int moves){
        return moves * scoreBase;
    }
}
