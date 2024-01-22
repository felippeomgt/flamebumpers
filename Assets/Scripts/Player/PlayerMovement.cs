using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 lastDirection;
    private Player player;

    public Animator animator;
    public float moveSpeed;

    public Vector2 getLastDirection() { return lastDirection; }

    void setNewDirection(float x, float y)
    {
        lastDirection.x = direction.x;
        lastDirection.y = direction.y;
        direction.x = x;
        direction.y = y;
    }

    void setAnimationDirection()
    {
        KeyCode[] keys = player.getMovementKeys();

        if (Input.GetKeyDown(keys[0]) && !Input.GetKey(keys[2]))
        {
            setNewDirection(0.0f, 1.0f);
        }
        if (Input.GetKeyDown(keys[2]) && !Input.GetKey(keys[0]))
        {
            setNewDirection(0.0f, -1.0f);
        }
        if (Input.GetKeyDown(keys[1]) && !Input.GetKey(keys[3]))
        {
            setNewDirection(1.0f, 0.0f);
        }
        if (Input.GetKeyDown(keys[3]) && !Input.GetKey(keys[1]))
        {
            setNewDirection(-1.0f, 0.0f);
        }

        if (Input.GetKeyUp(keys[0]) && Input.anyKey && lastDirection.x != 0.0f && lastDirection.y != 1.0f)
        {
            setNewDirection(lastDirection.x, lastDirection.y);
        }
        if (Input.GetKeyUp(keys[2]) && Input.anyKey && lastDirection.x != 0.0f && lastDirection.y != -1.0f)
        {
            setNewDirection(lastDirection.x, lastDirection.y);
        }
        if (Input.GetKeyUp(keys[1]) && Input.anyKey && lastDirection.x != 1.0f && lastDirection.y != 0.0f)
        {
            setNewDirection(lastDirection.x, lastDirection.y);
        }
        if (Input.GetKeyUp(keys[3]) && Input.anyKey && lastDirection.x != -1.0f && lastDirection.y != 0.0f)
        {
            setNewDirection(lastDirection.x, lastDirection.y);
        }

        if (Input.GetKeyUp(keys[0]) && Input.GetKey(keys[2]))
        {
            setNewDirection(0.0f, -1.0f);
        }
        if (Input.GetKeyUp(keys[2]) && Input.GetKey(keys[0]))
        {
            setNewDirection(0.0f, 1.0f);
        }
        if (Input.GetKeyUp(keys[1]) && Input.GetKey(keys[3]))
        {
            setNewDirection(-1.0f, 0.0f);
        }
        if (Input.GetKeyUp(keys[3]) && Input.GetKey(keys[1]))
        {
            setNewDirection(1.0f, 0.0f);
        }

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }


    // Start is called before the first frame update
    void Awake()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        direction = new Vector2(0.0f, 1.0f);
        lastDirection = new Vector2(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal_P" + player.playerNumber), Input.GetAxis("Vertical_P" + player.playerNumber));

        animator.SetFloat("IsRunning", movement.magnitude);
        setAnimationDirection();

        float inputMagnitude = Mathf.Clamp01(movement.magnitude);
        movement.Normalize();

        player.transform.Translate(movement * moveSpeed * inputMagnitude * Time.deltaTime, Space.World);
        setNewDirection(direction.x, direction.y);
    }
}
