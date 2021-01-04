using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingMessage", menuName = "Ending Message", order = 52)]
public class EndingMessage : ScriptableObject
{
    public int endingNumber;
    [TextArea]
    public string message;
}
