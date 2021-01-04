using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Selectable
{
    public CanvasGroup errorTextGroup;
    public float coffeeCooldown = 2f;

    private bool onCooldown = false;

    override protected void SetTargetted(bool targetted)
    {
        base.SetTargetted(false);

        if (onCooldown) return;

        if (!SleepinessSystem.instance.IsCoffeeAvailable())
        {
            textGroup = errorTextGroup;
            outline.color = 1;
        }

        base.SetTargetted(targetted);
    }

    public override void Select()
    {
        if (!SleepinessSystem.instance.IsCoffeeAvailable()) return;
        if (onCooldown) return;

        StartCoroutine(CoffeeCooldown());

        base.Select();
    }

    private IEnumerator CoffeeCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(coffeeCooldown);
        onCooldown = false;
    }
}
