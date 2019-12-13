using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    private float blockOffset = 100;

    private Sprite sprite; // set this in the future by loading enumType from Resources/Sprites/...

    public BlockUI(Block block) {
        GameObject column = GameObject.Find("Column_" + block.X);
        GameObject blockPrefab = Resources.Load("Block") as GameObject;

        GameObject instance = Instantiate(blockPrefab, new Vector3(0, block.Y * blockOffset, 0), blockPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(column.transform, false);
		instance.name = string.Format("{0}{1}", block.X, block.Y);
    }
}
