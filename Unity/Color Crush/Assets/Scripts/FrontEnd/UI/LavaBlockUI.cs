using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlockUI : BlockUI
{
    private void Update() {
        float scrollSpeed = 0.5f;
        Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);

        image.material.mainTextureOffset = offset;
    }
}
