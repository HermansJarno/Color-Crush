using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBlock : Block
{
    public EmptyBlock(int x, int y) : base(x, y, BlockType.Empty) {
        // does not need to set anything
    }

    public override void CreateUI(){
        // does not need UI
    }

    public override void DeleteMyself(){
        // is never deleted
    }

    public override void DeleteMyself(float delay){
        // is never deleted
    }

}
