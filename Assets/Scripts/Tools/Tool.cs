using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public bool isEquipped;
    public bool isInRack;
    public bool isHighlighted;
    [field: SerializeField] public ToolDataSO Data { get; private set; }

    public ToolDataSO getData() { return Data; }

    public void moveToPosition(Transform newPos, Transform newParent)
    {
        transform.SetParent(newParent);
        transform.position = newPos.position;
        transform.rotation = newPos.rotation;
    }

    void Update()
    {
        if (transform.parent.name == "HandTool" || transform.parent.name == "BackTool")
        {
            isEquipped = true;
            isInRack = false;
        }
        else if (transform.parent.name == "ToolRack")
        {
            isEquipped = false;
            isInRack = true;
        }
        else
        {
            isEquipped = false;
            isInRack = false;
        }
    }
}
