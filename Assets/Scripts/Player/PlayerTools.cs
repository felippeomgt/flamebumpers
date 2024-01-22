using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    private Tool handTool;
    private Tool backTool;
    private int playerNumber;
    private bool toolRackInRange;
    private GameObject toolInRange;
    private Player player;
    private PlayerMovement movement;

    public Animator animator;

    void Awake()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        movement = transform.parent.gameObject.transform.Find("Movement").GetComponent<PlayerMovement>();
        handTool = transform.Find("HandTool").GetComponent<Tool>();
        backTool = transform.Find("BackTool").GetComponent<Tool>();
        toolRackInRange = false;
    }

    void useTool()
    {
        animator.SetFloat("Horizontal", movement.getLastDirection().x);
        animator.SetFloat("Vertical", movement.getLastDirection().y);
        animator.SetBool("IsUsing", true);
        movement.animator.SetBool("ClearState", true);
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode[] interactKey = player.getActionKeys();

        if (Input.GetKeyDown(interactKey[0]) && toolInRange)
        {
            // pickUpTool();
        }

        if (Input.GetKeyDown(interactKey[0]) && !toolInRange)
        {

            useTool();
        }

        if (Input.GetKeyDown(interactKey[0]) && !toolRackInRange)
        {
            // returnTool();
        }

        if (Input.GetKeyDown(interactKey[1]))
        {
            // switchTool();
        }

        if (Input.GetKeyUp(interactKey[0]) && animator.GetBool("IsUsing"))
        {
            movement.animator.SetBool("ClearState", false);
            animator.SetBool("IsUsing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool") && toolInRange != null)
        {
            toolInRange = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("ToolRack"))
        {
            toolRackInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            toolRackInRange = false;
        }
    }
}
