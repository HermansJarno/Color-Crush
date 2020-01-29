using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : Block
{
    private new LavaBlockUI blockUI;
    
    private Grid grid;

    public LavaBlock (int x, int y) : base(x, y, BlockType.Lava) {
        grid = GameObject.Find("GridController").GetComponent<GridController>().GetGrid();
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
        blockUI.CreateIndex(x,y);
        linkedUIGameObject = blockUI.gameObject;
    }

    public override void DeleteMyself(){
        blockUI.DeleteMyself();
    }

    public override bool AffectBlock(){
        List<Block> blocks = grid.GetListOfBlocksWithBlockTypeAroundBlock(this, BlockType.Color);
        Debug.Log("Options arround lava : " + blocks.Count);
        if(blocks.Count > 0){
            Block block = null;
            do{
                int rnd = Random.Range(0, blocks.Count);
                if(grid.RecentDestroyedLavaBlocks.Contains(blocks[rnd])){
                    blocks.RemoveAt(rnd);
                }else{
                    block = blocks[rnd];
                }
            }while(block == null && blocks.Count > 0);

            if(block != null){
                AffectBlockByLava(block);
                return true;
            } 
        }
        return false;
    }

    private void AffectBlockByLava(Block block){
		int x = block.X;
		int y = block.Y;
		if(grid.Blocks[x,y].BlockType == BlockType.Color){
			grid.Blocks[x,y].DeleteMyself();
			grid.Blocks[x,y] = new LavaBlock(x, y);
		}
	}

    public override void MoveToIndex(int x, int y){
        blockUI.MoveToIndex(x, y, Y);
        X = x;
        Y = y;
    }
}
