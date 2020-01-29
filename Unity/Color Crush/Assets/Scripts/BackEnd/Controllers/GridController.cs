using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    Grid grid = new Grid();
    Stack<ColorBlock> selectedBlocks = new Stack<ColorBlock>();
    ColorBlock lastSelectedBlock;

    private bool deadLock = false;

    private void Start() {
        grid.Initialize(5,5);
    }

    public void PassGameObject(GameObject passedGameObject) {
        if(passedGameObject.GetComponent<ColorBlockUI>() != null){
            ColorBlock currentBlock = passedGameObject.GetComponent<ColorBlockUI>().GetBlock();
            processBlock(currentBlock);
        }
    }

    private void processBlock(ColorBlock block){
        if(selectedBlocks.Count == 0){
            AddLatestBlockToStack(block);
        } else if(block.EqualsColorType(lastSelectedBlock) && block.Touches(lastSelectedBlock)){
            if(!block.Equals(lastSelectedBlock)){
                if(selectedBlocks.Count > 1){
                    ColorBlock poppedBlock = selectedBlocks.Pop();
                    
                    if(block.Equals(selectedBlocks.Peek())){
                        poppedBlock.ToggleSelected();
                        lastSelectedBlock = block;
                    }else{
                        selectedBlocks.Push(poppedBlock);
                        AddLatestBlockToStack(block);
                    }
                }else{
                    AddLatestBlockToStack(block);
                }
            }
        }
        //DebugCurrentSelectedBlocks();
    }

    private void DebugCurrentSelectedBlocks(){
        string list = "";
        Block[] blockList = selectedBlocks.ToArray();
        for (int i = 0; i < blockList.Length; i++)
        {
            if(i > 0 && i != blockList.Length) list += ",";
            list += blockList[i].Name;
        }          
        Debug.Log(list);
    }

    private void AddLatestBlockToStack(ColorBlock block){
        if(!selectedBlocks.Contains(block)){
            selectedBlocks.Push(block);
            lastSelectedBlock = block;
            block.ToggleSelected();
        }
    }

    public void SubmitSelection(){
        deadLock = true;
        UpdateGrid();
    }
    
    private void UpdateGrid(){
        if(selectedBlocks.Count == 1){
            selectedBlocks.Pop().ToggleSelected();
        }else if(selectedBlocks.Count > 1) {
            grid.DeleteBlocks(selectedBlocks.ToArray());
        }
        ReleaseSelection();
    }

    private void ReleaseSelection(){
        deadLock = false;
        lastSelectedBlock = null;
        selectedBlocks = new Stack<ColorBlock>();
    }

    public bool DeadLock { 
        get {
            return deadLock;
        } 
    }

    public ColorBlock[] GetSelectedBlocks(){
        return selectedBlocks.ToArray();
    }

    public Grid GetGrid(){
        return grid;
    }
}
