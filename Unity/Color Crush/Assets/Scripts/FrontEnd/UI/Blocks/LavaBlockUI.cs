using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavaBlockUI : BlockUI
{

    private float scrollSpeed = 0.05f;

    private new LavaBlock block;

    private RawImage rawImage;

    private bool visable = true;

    public void Initialize(LavaBlock block, bool delay = false) {
        this.block = block;
        Transform mask = gameObject.transform.Find("Mask");
        rawImage = mask.Find("Lava").gameObject.GetComponent<RawImage>();
        if(delay) Invoke("ToggleVisibility", GameObject.FindObjectOfType<GridController>().GetGrid().MaxDelay * 2);
    }

    public override void MoveToIndex(int x, int y, int originalY, int nthMove){
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, false);
        transform.SetSiblingIndex(y);

		gameObject.name = string.Format("{0}{1}", x, y);


        int steps = originalY - y;
        MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        moveBlock.BeginLerp(transform.localPosition, new Vector3(0, block.BlockOffset * y, 0), moveSpeed, delay * nthMove, steps);
    }

    public void MoveToIndex(int x, int y, int originalY, int nthMove, bool swap){
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, true);
        transform.SetSiblingIndex(y);

		gameObject.name = string.Format("{0}{1}", x, y);


        int steps = originalY - y;
        MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        moveBlock.BeginLerp(transform.localPosition, new Vector3(0, block.BlockOffset * y, 0), moveSpeed, delay * nthMove, steps);
    }

    public override void CreateIndex(int x, int y){
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, false);

		gameObject.name = string.Format("{0}{1}", x, y);

        int highestY = FindObjectOfType<GridController>().GetGrid().YLength;
        int steps = highestY - y;
        MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        moveBlock.BeginLerp(new Vector3(0, block.BlockOffset * highestY, 0), new Vector3(0, block.BlockOffset * y, 0), moveSpeed, 0f, delay * y, steps);
    }

    private void Update() {
        float xOffset = Time.time * scrollSpeed;
        rawImage.uvRect = new Rect(xOffset, xOffset, rawImage.uvRect.size.x, rawImage.uvRect.size.y);
    }

    public void ToggleVisibility(){
        visable = !visable;
        Transform mask = gameObject.transform.Find("Mask");
        mask.GetComponent<Image>().enabled = visable;
        mask.Find("Lava").gameObject.GetComponent<RawImage>().enabled = visable;
        gameObject.transform.Find("Border").GetComponent<Image>().enabled = visable;
    }
}
