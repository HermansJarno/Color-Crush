using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlockUI : BlockUI
{
    public bool selected = false;
    private Sprite selectedSprite;

    private new ColorBlock block;

    public void Initialize(ColorBlock block, string spriteType) {
        this.block = block;
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

    public new ColorBlock GetBlock(){
        return block;
    }
}
