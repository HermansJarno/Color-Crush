using System.Collections;
using System.Collections.Generic;


public abstract class Block 
{
    private int x;
    private int y;
    private BlockType blockType;
    private BlockUI blockUI;

    public Block(int X, int Y, BlockType type){
        x = X;
        y = Y;
        blockType = type;
        blockUI = new BlockUI(this);
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
