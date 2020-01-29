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
	private List<Block> recentDestroyedLavaBlocks;

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
				int rnd = Random.Range(0,4);
                if(rnd == 2){
                    blocks[x,y] = new LavaBlock(x,y);
                }else if(rnd == 3){
					blocks[x,y] = new IceBlock(x,y);
                }else{
					blocks[x,y] = new ColorBlock(x,y, (ColorType)Random.Range(1,7));
				}
            }
        }
    }

    public void DeleteBlocks(Block[] blocksToDelete){
		recentDestroyedLavaBlocks = new List<Block>();
        for (int i = 0; i < blocksToDelete.Length; i++)
        {
            DestroyNearBlocks(blocksToDelete[i]);
			blocksToDelete[i].LowerLifes();
			if(blocksToDelete[i].NoLifesLeft()){
				blocks[blocksToDelete[i].X, blocksToDelete[i].Y].DeleteMyself();
				blocks[blocksToDelete[i].X, blocksToDelete[i].Y] = null;
			}
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
		UpdateLavaBlocks();
        if(!CheckIfMovesLeftInGrid()){
           	if(CheckForPotentialMoveCombination()){
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

	private bool CheckForPotentialMoveCombination(){
		int yellowCount = GetNumberOfBlocksOfColorType(ColorType.Yellow);
		int redCount = GetNumberOfBlocksOfColorType(ColorType.Red);
		int blueCount = GetNumberOfBlocksOfColorType(ColorType.Blue);
		int greenCount = GetNumberOfBlocksOfColorType(ColorType.Green);
		int cyanCount = GetNumberOfBlocksOfColorType(ColorType.Cyan);
		int magentaCount = GetNumberOfBlocksOfColorType(ColorType.Magenta);

		return yellowCount > 1 || redCount > 1 || blueCount > 1 || greenCount > 1 || cyanCount > 1 || magentaCount > 1;
	}

   	private bool CheckIfMovesLeftInGrid(){
		bool movesLeftInGrid = false;

		for (int x = 0; x < xLength; x++)
		{
			for (int y = 0; y < yLength; y++)
			{
				if(blocks[x,y] != null && blocks[x,y].Selectable() && CheckIfMovesAroundIndex(blocks[x,y])){
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

    private bool CheckIfMovesAroundIndex(Block currentBlock){
		bool optionalMovesLeft = false;

		if ((currentBlock.X % 2) == 0) {
			optionalMovesLeft = CheckForOptionalMoves(currentBlock, xOffsets, yEvenOffsets);
		} else {
			optionalMovesLeft = CheckForOptionalMoves(currentBlock, xOffsets, yOddOffsets);
		}
		return optionalMovesLeft;
	}

    private bool CheckForOptionalMoves(Block currentBlock, int[] xOffsets, int[] yOffsets){
        bool optionalMovesLeft = false;
		for (int i = 0; i < xOffsets.Length; i++)
		{
			if (CheckIfIndexIsInTheGrid(currentBlock.X + xOffsets[i], currentBlock.Y + yOffsets[i])){
				int x = currentBlock.X + xOffsets[i];
				int y = currentBlock.Y + yOffsets[i];
				if(blocks[x, y] != null && blocks[x, y].EqualsColorType(currentBlock)) optionalMovesLeft = true;
			}
			if(optionalMovesLeft){
				break;
			}
		}
        return optionalMovesLeft;
    }

    private bool CheckIfIndexIsInTheGrid(int x, int y){
		bool isInTheGrid = false;
		if (xLength > x && yLength > y)
		{
			isInTheGrid |= ((0 <= x) && (0 <= y));
		}
		return isInTheGrid;
	}

    private void DestroyNearBlocks(Block block)
	{
		if ((block.X % 2) == 0) {
            CheckForBreakableBlocks(block, xOffsets, yEvenOffsets);
		} else {
			CheckForBreakableBlocks(block, xOffsets, yOddOffsets);
		}
	}

    private void CheckForBreakableBlocks(Block block, int[] xOffsets, int[] yOffsets){
		for (int i = 0; i < xOffsets.Length; i++)
		{
			int x = block.X + xOffsets[i];
			int y = block.Y + yOffsets[i];
			if (CheckIfIndexIsInTheGrid(x,y) && blocks[x, y] != null && blocks[x, y].Breakable())
			{
				if(blocks[x, y].BlockType == BlockType.Lava){
					recentDestroyedLavaBlocks.Add(blocks[x, y]);
				}

				blocks[x, y].LowerLifes();
				if(blocks[x, y].NoLifesLeft()){
					blocks[x, y].DeleteMyself();
                	blocks[x, y] = null;
				}
			}
		}
    }

	public List<Block> GetListOfBlocksWithBlockTypeAroundBlock(Block block, BlockType blockType){
		List<Block> filteredBlocks = new List<Block>();
		int[] yOffsets = GetYOffset(block);
		for (int i = 0; i < xOffsets.Length; i++)
		{
			if (CheckIfIndexIsInTheGrid(block.X + xOffsets[i], block.Y + yOffsets[i])){
				Block tempBlock = blocks[block.X + xOffsets[i], block.Y + yOffsets[i]];
				if(tempBlock != null && tempBlock.BlockType == blockType){
					filteredBlocks.Add(tempBlock);
				} 
			}
		}
		return filteredBlocks;
	}

	private int[] GetYOffset(Block block){
		return block.X % 2 == 0 ? yEvenOffsets : yOddOffsets;
	}

    private void Shuffle()
	{
		Debug.Log("Shuffle grid");
        for (int x = 0; x < xLength; x++) {
			for (int y = 0; y < yLength; y++)
			{
				if (blocks[x,y] != null && blocks[x,y].Selectable()) {
					Block block = SearchSelectableBlock(x, y);
					if(block != null && block.Selectable()){
						Debug.Log(block.BlockType);
						Debug.Log("Shuffling: " + x + "-" + y + "&" + block.X + "-" + block.Y);
						Swap(blocks[x,y], block);
					} 
				}
			}
		}

        if(CheckIfMovesLeftInGrid()){
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
		}while(blocks[newX,newY] == null && newX == x && newY == y && !blocks[newX,newY].Selectable());
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
				if(BlockEqualsBlockType(blocks[x,y], blockType)) count++;
			}
		}
		return count;
	}

	private bool BlockEqualsBlockType(Block block, BlockType blockType){
		return block != null && block.BlockType == blockType;
	}

	private bool BlockEqualsColorType(Block block, ColorType colorType){
		return block != null && block.ColorType == colorType;
	}

	private int GetNumberOfBlocksOfColorType(ColorType colorType){
		int count = 0;
		for (int x = 0; x < xLength; x++) {
			for (int y = 0; y < yLength; y++)
			{
				if(BlockEqualsColorType(blocks[x,y], colorType)) count++;
			}
		}
		return count;
	}

	private void UpdateLavaBlocks(){
		if(GetNumberOfBlocksOfType(BlockType.Color) > 0){
			List<Block> list = GetListOfBlocksOfType(BlockType.Lava);
			bool blockAffectedByLava = false;
			if(list.Count > 0){
				do{
					int randomIndex = Random.Range(0, list.Count);
					blockAffectedByLava = blocks[list[randomIndex].X, list[randomIndex].Y].AffectBlock();
					if(!blockAffectedByLava) list.RemoveAt(randomIndex);
					Debug.Log(list.Count);
				} while(!blockAffectedByLava && list.Count > 0);
			}
		}
	}

	private List<Block> GetListOfBlocksOfType(BlockType blockType){
		List<Block> list = new List<Block>();
		for (int x = 0; x < xLength; x++) {
			for (int y = 0; y < yLength; y++)
			{
				if(BlockEqualsBlockType(blocks[x,y], blockType)) list.Add(blocks[x,y]);
			}
		}
		return list;
	}

	public List<Block> RecentDestroyedLavaBlocks{
		get{
			return recentDestroyedLavaBlocks;
		}
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
