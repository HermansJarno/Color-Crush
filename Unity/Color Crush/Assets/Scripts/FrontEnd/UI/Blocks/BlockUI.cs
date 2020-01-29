using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BlockUI : MonoBehaviour
{
    protected Sprite sprite; // set this in the future by loading enumType from Resources/Sprites/...
    
    protected Image image;

    protected Block block;

    protected float moveSpeed = 6f;

    protected float moveDelay = 0.1f;

    public abstract void MoveToIndex(int x, int y, int originalY);

    public abstract void CreateIndex(int x, int y);

    public void DeleteMyself(){
        GameObject.Destroy(this.gameObject);
    }

    protected virtual void LoadSprites(string spriteType){
        sprite = Resources.Load<Sprite>("Sprites/Blocks/Regular/" + spriteType);
    }

    public Block GetBlock(){
        return block;
    }
}
