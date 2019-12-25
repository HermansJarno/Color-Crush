using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    protected int x;
    protected int y;
    protected BlockType blockType;
    protected BlockUI blockUI;
    protected float blockOffset = 100;

    public Block(int x, int y, BlockType type){
        this.x = x;
        this.y = y;
        blockType = type;
    }

    public virtual void CreateUI(){
        GameObject column = GameObject.Find("Column_" + x);
        GameObject blockPrefab = Resources.Load("Block") as GameObject;

        GameObject instance = MonoBehaviour.Instantiate(blockPrefab, new Vector3(0, y * blockOffset, 0), blockPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(column.transform, false);
		instance.name = string.Format("{0}{1}", x, y);

        blockUI = instance.GetComponent<BlockUI>();
        blockUI.Initialize(this, blockType.ToString());
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

    public BlockType BlockType{
        get { return blockType; }
    }

}
