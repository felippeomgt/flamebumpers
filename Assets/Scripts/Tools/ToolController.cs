using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpriteKeys
{
    HIGHLIGHTED,
    UNEQUIPPED,
    HAND_TOOL,
    BACK_TOOL,
}
enum SpriteOrder
{
    RACK,
    GROUND,
    PLAYER,
    EQUIPPED,
}
public class ToolController : MonoBehaviour
{
    public SpriteRenderer spriteR;

    private Tool tool;
    private ToolDataSO data;

    void Awake()
    {
        tool = transform.parent.gameObject.GetComponent<Tool>();
        data = tool.getData();
    }

    void loadCorrectSprite()
    {
        string parentName = tool.transform.parent.name ?? "";

        switch (parentName)
        {
            case "HandTool":
                spriteR.sprite = data.Sprites[(int)SpriteKeys.HAND_TOOL];
                break;
            case "BackTool":
                spriteR.sprite = data.Sprites[(int)SpriteKeys.BACK_TOOL];
                break;
            default:
                if (tool.isHighlighted)
                {
                    spriteR.sprite = data.Sprites[(int)SpriteKeys.HIGHLIGHTED];
                    break;
                }
                spriteR.sprite = data.Sprites[(int)SpriteKeys.UNEQUIPPED];
                break;
        }

        if (tool.isEquipped)
        {
            spriteR.GetComponent<Renderer>().sortingOrder = (int)SpriteOrder.EQUIPPED;
            tool.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (tool.isInRack)
        {
            spriteR.GetComponent<Renderer>().sortingOrder = (int)SpriteOrder.RACK;
            tool.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            spriteR.GetComponent<Renderer>().sortingOrder = (int)SpriteOrder.GROUND;
            tool.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    void Update()
    {
        if (data == null)
        {
            spriteR.sprite = null;
            spriteR.GetComponent<Renderer>().sortingOrder = (int)SpriteOrder.GROUND;
            return;
        }
        loadCorrectSprite();
    }
}
