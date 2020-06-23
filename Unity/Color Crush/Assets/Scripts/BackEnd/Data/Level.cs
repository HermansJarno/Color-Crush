using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Level 
{
    Dictionary<BlockType, int> levelGoals = new Dictionary<BlockType, int>();
    Dictionary<ColorType, int> colorGoals = new Dictionary<ColorType, int>();
    int index;
    int numberOfMoves;
    BlockType[,] blocks;

    public Level (int levelIndex, int numberOfMoves, BlockType[,] blocks){
        this.index = levelIndex;
        this.numberOfMoves = numberOfMoves;
        this.blocks = blocks;
    }

    public void AddEntryToLevelGoals(BlockType blockType, int quantity){
        levelGoals.Add(blockType, quantity);
    }

    public void AddEntryToColorGoals(ColorType colorType, int quantity){
        colorGoals.Add(colorType, quantity);
    }

    public Dictionary<BlockType, int> LevelGoals{
        get{
            return levelGoals;
        }
    }

    public Dictionary<ColorType, int> ColorGoals{
        get{
            return colorGoals;
        }
    }

    public BlockType[,] Blocks{
        get {
            return blocks;
        }
        set {
            blocks = value;
        }
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
