using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cakeslice;

public class Selectable : MonoBehaviour
{
    public CanvasGroup targettedTextGroup;
    public UnityEvent onSelected = new UnityEvent();

    protected Outline outline;
    protected CanvasGroup textGroup;

    void Awake()
    {
        outline = GetComponent<Outline>();
        textGroup = targettedTextGroup;
    }

    protected virtual void Update()
    {
        SetTargetted(LookSelector.instance.GetTarget() == this);
    }

    public virtual void Select()
    {
        onSelected.Invoke();
    }

    protected virtual void SetTargetted(bool targetted)
    {
        EnableText(targetted);
        outline.enabled = targetted;
    }

    protected void EnableText(bool enabled)
    {
        textGroup.alpha = enabled ? 1 : 0;
        textGroup.blocksRaycasts = enabled;
        textGroup.interactable = enabled;
    }
}
