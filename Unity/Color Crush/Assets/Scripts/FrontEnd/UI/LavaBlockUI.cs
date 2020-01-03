using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavaBlockUI : BlockUI
{

    private float scrollSpeed = 0.05f;

    private new LavaBlock block;

    private RawImage rawImage;

    public void Initialize(LavaBlock block) {
        this.block = block;
        Transform mask = gameObject.transform.Find("Mask");
        rawImage = mask.Find("Lava").gameObject.GetComponent<RawImage>();
    }

    public override void MoveToIndex(int x, int y){
        //MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        //moveBlock.BeginLerp();
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, false);
        
		gameObject.name = string.Format("{0}{1}", x, y);

        transform.localPosition = new Vector3(0, block.BlockOffset * y, 0);
    }

    private void Update() {
        float xOffset = Time.time * scrollSpeed;
        rawImage.uvRect = new Rect(xOffset, xOffset, rawImage.uvRect.size.x, rawImage.uvRect.size.y);
    }
}
