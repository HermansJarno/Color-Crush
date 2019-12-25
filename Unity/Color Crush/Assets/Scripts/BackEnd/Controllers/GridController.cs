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
        } else if(block.EqualsColorType(lastSelectedBlock)){
            if(!block.Equals(lastSelectedBlock)){
                if(selectedBlocks.Count > 1){
                    ColorBlock poppedBlock = selectedBlocks.Pop();

                    // deselecting
                    if(block.Equals(selectedBlocks.Peek())){
                        poppedBlock.ToggleSelected();
                    }else{
                        selectedBlocks.Push(poppedBlock);
                        AddLatestBlockToStack(block);
                    }
                }else{
                    AddLatestBlockToStack(block);
                }
            }
        }
    }

    private void AddLatestBlockToStack(ColorBlock block){
        selectedBlocks.Push(block);
        lastSelectedBlock = block;
        block.ToggleSelected();
    }

    public void SubmitSelection(){
        deadLock = true;
        UpdateGrid();
    }
    
    private void UpdateGrid(){
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
}
