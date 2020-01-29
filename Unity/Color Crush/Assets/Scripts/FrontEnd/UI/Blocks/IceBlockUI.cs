using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceBlockUI : BlockUI
{
    private new IceBlock block;

    public void Initialize(IceBlock block) {
        this.block = block;
    }

    public void UpdateImage(int lifes){

    }

    public override void MoveToIndex(int x, int y, int originalY){
        // never moves
    }

    public override void CreateIndex(int x, int y){
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, false);

		gameObject.name = string.Format("{0}{1}", x, y);

        int highestY = FindObjectOfType<GridController>().GetGrid().YLength;
        int steps = highestY - y;
        MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        moveBlock.BeginLerp(new Vector3(0, block.BlockOffset * highestY, 0), new Vector3(0, block.BlockOffset * y, 0), moveSpeed, 0f, moveDelay * y, steps);
    }

}
