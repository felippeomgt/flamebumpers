using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public KeyCode[] getMovementKeys()
    {
        switch (playerNumber)
        {
            case 2:
                return new[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A };
            case 3:
                return new[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A };
            case 4:
                return new[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A };
            default:
                return new[] { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A };
        }
    }

    public KeyCode[] getActionKeys()
    {
        switch (playerNumber)
        {
            case 2:
                return new[] { KeyCode.E, KeyCode.Q, KeyCode.R };
            case 3:
                return new[] { KeyCode.E, KeyCode.Q, KeyCode.R };
            case 4:
                return new[] { KeyCode.E, KeyCode.Q, KeyCode.R };
            default:
                return new[] { KeyCode.E, KeyCode.Q, KeyCode.R };
        }
    }
}
