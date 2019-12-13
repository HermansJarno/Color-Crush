using System.Collections;
using System.Collections.Generic;

public class ColorBlock : Block
{
    private ColorType colorType;

    public ColorBlock(int X, int Y, ColorType color) : base (X, Y, BlockType.Color){
        colorType = color;
    }

    public ColorType ColorType{
        get { return colorType; }
    }
}
