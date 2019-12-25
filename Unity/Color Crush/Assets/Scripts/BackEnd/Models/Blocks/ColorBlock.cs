using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : Block
{
    private bool selected = false;
    private ColorType colorType;
    
    private new ColorBlockUI blockUI;

    public ColorBlock(int x, int y, ColorType color) : base (x, y, BlockType.Color){
        colorType = color;
        CreateUI();
    }

    public ColorType ColorType{
        get { return colorType; }
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

    public bool EqualsColorType(ColorBlock block){
        return block != null && colorType == block.ColorType;
    }

    public override void CreateUI(){
        GameObject column = GameObject.Find("Column_" + x);
        GameObject blockPrefab = Resources.Load("ColorBlock") as GameObject;

        GameObject instance = MonoBehaviour.Instantiate(blockPrefab, new Vector3(0, y * blockOffset, 0), blockPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(column.transform, false);
		instance.name = string.Format("{0}{1}", x, y);

        blockUI = instance.GetComponent<ColorBlockUI>();
        blockUI.Initialize(this, colorType.ToString());
    }

    public void ToggleSelected(){
        selected = !selected;
        blockUI.ToggleSelection(selected);
    }
}
