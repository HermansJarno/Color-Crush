using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : Block
{
    private new LavaBlockUI blockUI;

    public LavaBlock (int x, int y) : base(x, y, BlockType.Lava) {
        CreateUI();
    }

    public override void CreateUI(){
        GameObject column = GameObject.Find("Column_" + x);
        GameObject blockPrefab = Resources.Load("LavaBlock") as GameObject;

        GameObject instance = MonoBehaviour.Instantiate(blockPrefab, new Vector3(0, y * blockOffset, 0), blockPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(column.transform, false);
		instance.name = string.Format("{0}{1}", x, y);

        blockUI = instance.GetComponent<LavaBlockUI>();
        blockUI.Initialize(this);
    }

    public override void DeleteMyself(){
        blockUI.DeleteMyself();
    }

    public override void MoveToIndex(int x, int y){
        blockUI.MoveToIndex(x, y);
        X = x;
        Y = y;
    }
}
