using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newToolData", menuName = "Data/Tool Data/Basic Tool Data", order = 0)]
public class ToolDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}
