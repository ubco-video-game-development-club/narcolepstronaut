using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Selectable : MonoBehaviour
{
    public CanvasGroup textGroup;

    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        bool isTargetted = LookSelector.instance.GetTarget() == this;
        EnableText(isTargetted);
        outline.enabled = isTargetted;
    }

    public void EnableText(bool enabled)
    {
        textGroup.alpha = enabled ? 1 : 0;
        textGroup.blocksRaycasts = enabled;
        textGroup.interactable = enabled;
    }
}
