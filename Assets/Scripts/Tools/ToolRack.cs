using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRack : MonoBehaviour
{
    public SpriteRenderer spriteR;
    public bool isHighlighted;
    public Sprite[] sprites;

    void Update()
    {
        if (isHighlighted && !hasTheSameSprite(SpriteKeys.HIGHLIGHTED))
        {
            spriteR.sprite = sprites[(int)SpriteKeys.HIGHLIGHTED];
        }

        if (!isHighlighted && !hasTheSameSprite(SpriteKeys.UNEQUIPPED))
        {
            spriteR.sprite = sprites[(int)SpriteKeys.UNEQUIPPED];
        }
    }

    bool hasTheSameSprite(SpriteKeys key)
    {
        return spriteR.sprite == sprites[(int)key];
    }
}
