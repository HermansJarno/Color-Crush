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

    public override void MoveToIndex(int x, int y){
        //MoveBlock moveBlock = gameObject.AddComponent<MoveBlock>();
        //moveBlock.BeginLerp();
        GameObject column = GameObject.Find("Column_" + x);

        transform.SetParent(column.transform, false);

		gameObject.name = string.Format("{0}{1}", x, y);

        transform.localPosition = new Vector3(0, block.BlockOffset * y, 0);
    }

    public new ColorBlock GetBlock(){
        return block;
    }
}
