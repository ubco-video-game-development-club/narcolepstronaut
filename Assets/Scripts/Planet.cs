using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Selectable
{
    public EndingMessage endingMessage;
    public Vector3 textOffset;
    public Vector3 rotationAxis;
    public float rotationSpeed;

    protected override void Update()
    {
        transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);

        Vector3 lookPos = LookSelector.instance.transform.position;
        Vector3 lookNormal = (lookPos - transform.position);
        Vector3 worldOffset = Vector3.ProjectOnPlane(textOffset, lookNormal);
        targettedTextGroup.transform.position = transform.position + worldOffset;
        targettedTextGroup.transform.rotation = Quaternion.LookRotation(-lookNormal, LookSelector.instance.transform.up);

        base.Update();
    }

    public override void Select()
    {
        EndingMenu.instance.PlayEnding(endingMessage.message);
        AchievementSystem.instance.AcquireAchievement(endingMessage.endingNumber);
    }
}
