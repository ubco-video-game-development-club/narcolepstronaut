using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaikaScreen : Selectable
{
    public CanvasGroup errorTextGroup;

    override protected void SetTargetted(bool targetted)
    {
        EnableText(false);
        textGroup = LaikaHUD.instance.IsLaikaBusy() ? errorTextGroup : targettedTextGroup;

        base.SetTargetted(targetted);
    }
}
