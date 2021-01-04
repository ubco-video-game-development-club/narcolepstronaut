using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : Selectable
{
    public float wireCooldown = 2f;

    private bool onCooldown = false;

    override protected void SetTargetted(bool targetted)
    {
        base.SetTargetted(false);

        if (onCooldown) return;

        base.SetTargetted(targetted);
    }

    public override void Select()
    {
        if (onCooldown) return;

        StartCoroutine(WireCooldown());

        base.Select();
    }

    private IEnumerator WireCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(wireCooldown);
        onCooldown = false;
    } 
}
