using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : Block
{
    private bool selected = false;
    
    private new ColorBlockUI blockUI;

    public ColorBlock(int x, int y, ColorType color) : base (x, y, BlockType.Color){
        colorType = color;
        CreateUI();
    }

    public ColorBlock(int x, int y) : base (x, y, BlockType.Color){
        colorType = (ColorType)Random.Range(1,7);
        CreateUI();
    }

    public bool Equals(ColorBlock block)
    {
        if (EqualsColorType(block))
        {
            return block.X == x && block.Y == y;         
        } else {
            return false;
        }
    }

    public override void CreateUI(){
        GameObject column = GameObject.Find("Column_" + x);
        GameObject blockPrefab = Resources.Load("ColorBlock") as GameObject;

        GameObject instance = MonoBehaviour.Instantiate(blockPrefab, new Vector3(0, y * blockOffset, 0), blockPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(column.transform, false);
		instance.name = string.Format("{0}{1}", x, y);

        blockUI = instance.GetComponent<ColorBlockUI>();
        blockUI.Initialize(this, colorType.ToString());
        blockUI.CreateIndex(x,y);
        linkedUIGameObject = blockUI.gameObject;
    }

    public override void DeleteMyself(){
        blockUI.DeleteMyself();
    }

    public override void MoveToIndex(int x, int y){
        blockUI.MoveToIndex(x, y, Y);
        X = x;
        Y = y;
    }

    public override bool AffectBlock(){
        return false;
    }

    public void ToggleSelected(){
        selected = !selected;
        blockUI.ToggleSelection(selected);
    }
}
