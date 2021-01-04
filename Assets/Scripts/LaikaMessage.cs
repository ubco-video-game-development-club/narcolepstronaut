using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaikaMessage", menuName = "LAI-KA Message", order = 51)]
public class LaikaMessage : ScriptableObject
{
    public float sleepinessBoost;
    [TextArea]
    public string message;
}
