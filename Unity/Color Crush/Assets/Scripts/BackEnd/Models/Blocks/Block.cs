using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block
{
    protected int x;
    protected int y;
    protected BlockType blockType;
    protected ColorType colorType;
    protected BlockUI blockUI;
    protected float blockOffset = 100;
    protected int lifes = 1;
    protected GameObject linkedUIGameObject;

    public Block(int x, int y, BlockType type){
        this.x = x;
        this.y = y;
        blockType = type;
    }

    public bool Equals(Block block)
    {
        if (block == null || blockType != block.BlockType)
        {
            return false;
        } else {
            return block.X == x && block.Y == y;
        }
    }

    public bool EqualsBlockType(Block block)
    {
        return block != null && blockType == block.BlockType;
    }

    public bool EqualsColorType(Block block)
    {
        return block != null && colorType == block.ColorType;
    }

    public bool SameIndex(Block block)
    {
        if (block == null)
        {
            return false;
        } else {
            return block.X == x && block.Y == y;
        }
    }

    public bool Touches(Block block){
        if(block == null){
            return false;
        }
        else if (((block.X + 1 == x) || (block.X - 1 == x) || (block.X == x)))
		{
			if (((block.Y + 1 == y) || (block.Y - 1 == y) || (block.Y == y)))
			{
				return true;
			}
		}
        return false;
    }

    public abstract void CreateUI();
    public abstract void DeleteMyself();
    public abstract void DeleteMyself(float delay);

    public virtual void LowerLifes(){
        lifes--;   
    }

    public virtual bool AffectBlock(){
        return false;
    }

    public virtual void MoveToIndex(int x, int y){
        this.x = x;
        this.y = y;
    }

    public virtual void MoveToIndex(int x, int y, int nthMove){
        this.x = x;
        this.y = y;
    }

    public virtual void MoveToIndex(int x, int y, int nthMove, bool swap){
        this.x = x;
        this.y = y;
    }

    public bool Empty(){
        return blockType == BlockType.Empty;
    }

    public bool Moveable(){
        return blockType != BlockType.Ice;
    }

    public bool Selectable(){
        return blockType == BlockType.Color;
    }

    public bool Breakable(){
        return blockType == BlockType.Lava || blockType == BlockType.Ice;
    }

    public virtual bool NoLifesLeft(){
        return lifes == 0;
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public float BlockOffset {
        get { return blockOffset; }
    }

    public BlockType BlockType{
        get { return blockType; }
    }

    public ColorType ColorType{
        get { return colorType; }
    }

    public string Name {
        get {
            return x + "-" + y;
        }
    }

    public GameObject GetLinkedUIGameObject(){
        return linkedUIGameObject;
    }
}
