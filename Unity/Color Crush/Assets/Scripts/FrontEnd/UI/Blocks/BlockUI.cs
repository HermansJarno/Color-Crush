using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BlockUI : MonoBehaviour
{
    protected Sprite sprite; // set this in the future by loading enumType from Resources/Sprites/...
    
    protected Image image;

    protected Block block;

    protected float moveSpeed = 4f;

    protected float delay = 0.05f;

    public abstract void MoveToIndex(int x, int y, int originalY, int nthMove);

    public abstract void CreateIndex(int x, int y);

    public void DeleteMyself(){
        GameObject.Destroy(this.gameObject);
    }

    public void DeleteMyself(float delay){
        GameObject.Destroy(this.gameObject, delay);
    }

    protected virtual void LoadSprites(string spriteType){
        sprite = Resources.Load<Sprite>("Sprites/Blocks/Regular/" + spriteType);
    }

    public Block GetBlock(){
        return block;
    }

    public float Delay{
        get{
            return delay;
        }
    }
}
