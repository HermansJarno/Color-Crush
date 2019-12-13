using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private Block[,] blocks;
    private int xLength;
    private int yLength;
    private GridUI gridUI; 

    public void Initialize(int xLength, int yLength){
        this.xLength = xLength;
        this.yLength = yLength;
        gridUI = GameObject.FindObjectOfType<GridUI>();
        gridUI.Initialize(this);
        blocks = new Block[xLength, yLength];

        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                blocks[i,j] = new ColorBlock(i,j, (ColorType)Random.Range(1,7));
            }
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
