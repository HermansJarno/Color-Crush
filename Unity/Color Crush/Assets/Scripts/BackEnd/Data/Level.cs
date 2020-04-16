using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Level 
{
    Dictionary<BlockType, int> levelGoals = new Dictionary<BlockType, int>();
    int index;
    int numberOfMoves;

    public Level (int levelIndex, int numberOfMoves){
        this.index = levelIndex;
        this.numberOfMoves = numberOfMoves;
    }

    public void AddEntryToLevelGoals(BlockType blockType, int quantity){
        levelGoals.Add(blockType, quantity);
    }

    public int Index {
        get {
            return index;
        }
    }

    public int NumberOfMoves {
        get {
            return index;
        }
    }
}
