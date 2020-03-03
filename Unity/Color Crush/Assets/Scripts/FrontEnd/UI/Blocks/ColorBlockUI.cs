using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlockUI : BlockUI
{
    public bool selected = false;
    private Sprite selectedSprite;

    private new ColorBlock block;

    [SerializeField]
    private ColorType colorType;

    public void Initialize(ColorBlock block, string spriteType) {
        this.block = block;
        colorType = block.ColorType;
        image = gameObject.GetComponent<Image>();
        
        LoadSprites(spriteType);
        image.overrideSprite = sprite;
    }

    protected override void LoadSprites(string spriteType){
        sprite = Resources.Load<Sprite>("Sprites/Blocks/Regular/" + spriteType);
        selectedSprite = Resources.Load<Sprite>("Sprites/Blocks/Selected/" + spriteType);
    } 

    public void ToggleSelection(bool selected){
        this.selected = selected;
        if(selected){
            image.overrideSprite = selectedSprite;
        }else{
            image.overrideSprite = sprite;
        }
    }

    public override void MoveToIndex(int x, int y, int originalY, int nthMove){
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, false);
        transform.SetSiblingIndex(y);

        int steps = originalY - y;
        MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        moveBlock.BeginLerp(transform.localPosition, new Vector3(0, block.BlockOffset * y, 0), moveSpeed, delay * nthMove, steps);

        //transform.SetParent(column.transform, false);
        //transform.SetSiblingIndex(y);

		gameObject.name = string.Format("{0}{1}", x, y);
    }

    public void MoveToIndex(int x, int y, int originalY, int nthMove, bool swap){
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, true);

        int steps = originalY - y;
        MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        moveBlock.BeginLerp(transform.localPosition, new Vector3(0, block.BlockOffset * y, 0), moveSpeed, delay * nthMove, steps);

        transform.SetSiblingIndex(y);
		gameObject.name = string.Format("{0}{1}", x, y);
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

    public new ColorBlock GetBlock(){
        return block;
    }
}
