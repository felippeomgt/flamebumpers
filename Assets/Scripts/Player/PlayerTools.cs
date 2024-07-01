using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    private GameObject handTool;
    private GameObject backTool;
    private int playerNumber;
    private List<ToolRack> racksInRange;
    private List<Tool> toolsInRange;
    private Player player;
    private PlayerMovement movement;

    public Animator animator;

    private bool hasHandToolEquipped()
    {
        return handTool.transform.childCount > 0;
    }

    private bool hasBackToolEquipped()
    {
        return backTool.transform.childCount > 0;
    }

    private bool hasToolInRange()
    {
        return toolsInRange.Count > 0;
    }

    private bool hasRackInRange()
    {
        return racksInRange.Count > 0;
    }

    private void updateToolsInRageSelection()
    {
        if (hasToolInRange())
        {
            foreach (var t in toolsInRange)
            {
                t.isHighlighted = false;

                if (t.isEquipped) toolsInRange.Remove(t);
            }

            if (hasToolInRange()) (toolsInRange[^1]).isHighlighted = true;
        }
    }

    private void updateRacksInRangeSelection()
    {
        if (hasRackInRange())
        {
            foreach (var r in racksInRange)
            {
                r.isHighlighted = false;
            }

            if (hasRackInRange()) (racksInRange[^1]).isHighlighted = true;
        }
    }

    private void checkNewToolInRange(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("Tool"))
        {
            Tool enteringTool = collision.transform.parent.GetComponent<Tool>();

            if (!enteringTool.isEquipped && !toolsInRange.Contains(enteringTool))
            {
                toolsInRange.Add(enteringTool);
                updateToolsInRageSelection();
            }
        }
    }


    private void checkNewRackInRange(Collider2D collision)
    {

        if (collision.transform.gameObject.CompareTag("ToolRack"))
        {
            ToolRack enteringRack = collision.transform.gameObject.GetComponent<ToolRack>();

            if (!racksInRange.Contains(enteringRack))
            {
                racksInRange.Add(enteringRack);
                updateRacksInRangeSelection();
            }
        }
    }

    void Awake()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        movement = transform.parent.gameObject.transform.Find("Movement").GetComponent<PlayerMovement>();
        handTool = transform.Find("HandTool").gameObject;
        backTool = transform.Find("BackTool").gameObject;
        toolsInRange = new List<Tool>();
        racksInRange = new List<ToolRack>();
    }

    void useTool()
    {
        animator.SetFloat("Horizontal", movement.getLastDirection().x);
        animator.SetFloat("Vertical", movement.getLastDirection().y);
        animator.SetBool("IsUsing", true);
        movement.setClearState(true);
    }

    void switchTool()
    {
        Transform oldHand = null;
        Transform oldBack = null;

        if (hasHandToolEquipped())
        {
            oldHand = handTool.transform.GetChild(0);
        }

        if (hasBackToolEquipped())
        {
            oldBack = backTool.transform.GetChild(0);
        }

        if (oldHand) oldHand.GetComponent<Tool>().moveToPosition(backTool.transform, backTool.transform);
        if (oldBack) oldBack.GetComponent<Tool>().moveToPosition(handTool.transform, handTool.transform);
    }

    void pickUpTool()
    {
        Tool highlightedTool = toolsInRange[^1];

        if (hasHandToolEquipped() && !hasBackToolEquipped())
        {
            highlightedTool.moveToPosition(backTool.transform, backTool.transform);
            return;
        }

        if (hasHandToolEquipped() && hasBackToolEquipped())
        {
            GameObject grid = highlightedTool.transform.parent.gameObject;
            Transform oldHand = handTool.transform.GetChild(0);
            oldHand.GetComponent<Tool>().moveToPosition(highlightedTool.transform, grid.transform);
        }
        highlightedTool.moveToPosition(handTool.transform, handTool.transform);
        highlightedTool.isEquipped = true;
        toolsInRange.Remove(highlightedTool);
    }

    void dropTool()
    {
        if (!hasHandToolEquipped()) return;
        Transform oldHand = handTool.transform.GetChild(0);

        if (hasRackInRange())
        {
            ToolRack highlightedRack = racksInRange[^1];
            oldHand.GetComponent<Tool>().moveToPosition(highlightedRack.transform, highlightedRack.transform);
            return;
        }
        GameObject grid = transform.parent.parent.gameObject;
        oldHand.GetComponent<Tool>().moveToPosition(transform.parent.transform, grid.transform);
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode[] interactKey = player.getActionKeys();

        if (Input.GetKeyDown(interactKey[0]) && hasToolInRange()) pickUpTool();

        if (Input.GetKeyDown(interactKey[0]) && !hasToolInRange()) useTool();

        if (Input.GetKeyDown(interactKey[2])) dropTool();

        if (Input.GetKeyDown(interactKey[1])) switchTool();

        if (Input.GetKeyUp(interactKey[0]) && animator.GetBool("IsUsing"))
        {
            movement.setClearState(false);
            animator.SetBool("IsUsing", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        checkNewToolInRange(collision);
        checkNewRackInRange(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        checkNewToolInRange(collision);
        checkNewRackInRange(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("Tool"))
        {
            Tool leavingTool = collision.transform.parent.GetComponent<Tool>();

            if (toolsInRange.Contains(leavingTool))
            {
                leavingTool.isHighlighted = false;
                leavingTool.isEquipped = false;
                toolsInRange.Remove(leavingTool);
            }
            updateToolsInRageSelection();
        }

        if (collision.transform.gameObject.CompareTag("ToolRack"))
        {
            ToolRack leavingRack = collision.transform.gameObject.GetComponent<ToolRack>();

            if (racksInRange.Contains(leavingRack))
            {
                leavingRack.isHighlighted = false;
                racksInRange.Remove(leavingRack);
            }
            updateRacksInRangeSelection();
        }
    }
}
