using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private Block[,] blocks;
    private int xLength;
    private int yLength;
    private GridUI gridUI; 
    private int[] xOffsets = { -1, 1, 0, 0, -1, 1 };
    private int[] yEvenOffsets = { 0, 0, -1, 1, -1, -1 };
    private int[] yOddOffsets = { 0, 0, -1, 1, 1, 1};

    public void Initialize(int xLength, int yLength){
        this.xLength = xLength;
        this.yLength = yLength;
        gridUI = GameObject.FindObjectOfType<GridUI>();
        gridUI.Initialize(this);
        blocks = new Block[xLength, yLength];

        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                if(Random.Range(1,3) == 2){
                    blocks[x,y] = new LavaBlock(x,y);
                }else{
                    blocks[x,y] = new ColorBlock(x,y, (ColorType)Random.Range(1,7));
                }
            }
        }
    }

    public void DeleteBlocks(Block[] blocksToDelete){
        for (int i = 0; i < blocksToDelete.Length; i++)
        {
            DestroyNearBlocks(blocksToDelete[i]);
            blocks[blocksToDelete[i].X, blocksToDelete[i].Y] = null;
            blocksToDelete[i].DeleteMyself();
        }
        UpdateGrid();
    }

    private void UpdateGrid(){
        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                if(blocks[x,y] == null) {
                    bool newIndex;
                    Block block = FindNextBlock(x,y, out newIndex);
                    if(block != null){
                        blocks[block.X,block.Y] = null;
                        block.MoveToIndex(x,y);
                        blocks[x,y] = block;
                    }
                }
            }
        }
        if(!checkIfMovesLeftInGrid()){
           	if(checkForPotentialMoveCombination()){
				Shuffle();
			} else {
				Debug.Log("Doomed");
			}
        }
    }

    private Block FindNextBlock(int x, int y, out bool newIndex)
	{
		newIndex = false;
		if (y == (yLength - 1))
		{
			newIndex = true;
			return new ColorBlock(x,y);
		}
		else
		{
			y++;
			if (blocks[x, y] != null)
			{
                if(blocks[x, y].Moveable()){
                    return blocks[x, y];   
                }else{
                    return null;
                }
			}
			else
			{
				return FindNextBlock(x, y, out newIndex);
			}
		}
	}

	bool checkForPotentialMoveCombination(){
		int yellowCount = GetNumberOfBlocksOfColorType(ColorType.Yellow);
		int redCount = GetNumberOfBlocksOfColorType(ColorType.Red);
		int blueCount = GetNumberOfBlocksOfColorType(ColorType.Blue);
		int greenCount = GetNumberOfBlocksOfColorType(ColorType.Green);
		int cyanCount = GetNumberOfBlocksOfColorType(ColorType.Cyan);
		int magentaCount = GetNumberOfBlocksOfColorType(ColorType.Magenta);

		return yellowCount > 1 || redCount > 1 || blueCount > 1 || greenCount > 1 || cyanCount > 1 || magentaCount > 1;
	}

    bool checkIfMovesLeftInGrid(){
		bool movesLeftInGrid = false;

		for (int x = 0; x < xLength; x++)
		{
			for (int y = 0; y < yLength; y++)
			{
				if(blocks[x,y] != null && blocks[x,y].Selectable() && checkIfMovesAroundIndex(blocks[x,y])){
					movesLeftInGrid = true;
					break;
				}
			}
			if(movesLeftInGrid){
				break;
			}
		}
		return movesLeftInGrid;
	}

    private bool checkIfMovesAroundIndex(Block currentBlock){
		bool optionalMovesLeft = false;

		if ((currentBlock.X % 2) == 0) {
			optionalMovesLeft = checkForOptionalMoves(currentBlock, xOffsets, yEvenOffsets);
		} else {
			optionalMovesLeft = checkForOptionalMoves(currentBlock, xOffsets, yOddOffsets);
		}
		return optionalMovesLeft;
	}

    private bool checkForOptionalMoves(Block currentBlock, int[] xOffsets, int[] yOffsets){
        bool optionalMovesLeft = false;
		for (int i = 0; i < xOffsets.Length; i++)
		{
			if (checkIfIndexIsInTheGrid(currentBlock.X + xOffsets[i], currentBlock.Y + yOffsets[i])
			&& (blocks[currentBlock.X + xOffsets[i], currentBlock.Y + yOffsets[i]] != null && 
            blocks[currentBlock.X + xOffsets[i], currentBlock.Y + yOffsets[i]].ColorType == currentBlock.ColorType))
			{
				optionalMovesLeft = true;
				break;
			}
		}
        return optionalMovesLeft;
    }

    private bool checkIfIndexIsInTheGrid(int x, int y){
		bool isInTheGrid = false;
		if (xLength - 1 > x && yLength - 1 > y)
		{
			isInTheGrid |= ((0 <= x) && (0 <= y));
		}
		return isInTheGrid;
	}

    private void DestroyNearBlocks(Block block)
	{
		if ((block.X % 2) == 0) {
            checkForBreakableBlocks(block, xOffsets, yEvenOffsets);
		} else {
			checkForBreakableBlocks(block, xOffsets, yOddOffsets);
		}
	}

    private void checkForBreakableBlocks(Block block, int[] xOffsets, int[] yOffsets){
        Debug.Log("lets check");
		for (int i = 0; i < xOffsets.Length; i++)
		{
			if (checkIfIndexIsInTheGrid(block.X + xOffsets[i], block.Y + yOffsets[i])
			&& blocks[block.X + xOffsets[i], block.Y + yOffsets[i]] != null && blocks[block.X + xOffsets[i], block.Y + yOffsets[i]].Breakable())
			{
				blocks[block.X + xOffsets[i], block.Y + yOffsets[i]].DeleteMyself();
                blocks[block.X + xOffsets[i], block.Y + yOffsets[i]] = null;
			}
		}
    }

    private void Shuffle()
	{
		Debug.Log("Shuffle grid");
        for (int x = 0; x < xLength; x++) {
			for (int y = 0; y < yLength; y++)
			{
				if (blocks[x,y].Selectable()) {
					Block block = SearchSelectableBlock(x, y);
					Debug.Log(block.BlockType);
					Debug.Log("Shuffling: " + x + "-" + y + "&" + block.X + "-" + block.Y);
					Swap(blocks[x,y], block);
				}
			}
		}

        if(checkIfMovesLeftInGrid()){
			//Invoke("SetInputActive", 1f);
		}else{
			Debug.Log("Reshuffle");
			Shuffle();
		}
	}

	private Block SearchSelectableBlock(int x, int y){
		int newX = 0;
		int newY = 0;
		do{
			newX = Random.Range(0, xLength);
			newY = Random.Range(0, yLength);
		}while(newX == x && newY == y && !blocks[newX,newY].Selectable());
		return blocks[newX,newY];
	}

	private void Swap(Block block1, Block block2)
	{
		int x = block1.X;
		int y = block1.Y;
        block1.MoveToIndex(block2.X, block2.Y);
        block2.MoveToIndex(x, y);
        blocks[block1.X, block1.Y] = block1;
        blocks[block2.X, block2.Y] = block2;
		Debug.Log("Swapped");
	}

	private int GetNumberOfBlocksOfType(BlockType blockType){
		int count = 0;
		for (int x = 0; x < xLength; x++) {
			for (int y = 0; y < yLength; y++)
			{
				if(blocks[x,y].BlockType == blockType) count++;
			}
		}
		return count;
	}

	private int GetNumberOfBlocksOfColorType(ColorType colorType){
		int count = 0;
		for (int x = 0; x < xLength; x++) {
			for (int y = 0; y < yLength; y++)
			{
				if(blocks[x,y].ColorType == colorType) count++;
			}
		}
		return count;
	}

    public int XLength 
    {   
        get{return xLength;} 
    }

    public int YLength 
    {   
        get{return yLength;} 
    }

    public Block[,] Blocks{
        get{return blocks;} 
    }
}
