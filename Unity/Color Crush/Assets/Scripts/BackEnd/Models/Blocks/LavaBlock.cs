using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : Block
{
    public LavaBlock (int x, int y) : base(x, y, BlockType.Lava) {
        CreateUI();
    }
}
