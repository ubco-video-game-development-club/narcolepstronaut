using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Selectable
{
    public EndingMessage endingMessage;
    public Vector2 textOffset;
    public Vector3 rotationAxis;
    public float rotationSpeed;

    protected override void Update()
    {
        transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);

        Vector3 lookPos = LookSelector.instance.transform.position;
        Vector3 lookNormal = (transform.position - lookPos);
        Vector3 worldOffsetDir = Vector3.Project(textOffset, lookNormal).normalized;
        targettedTextGroup.transform.position = transform.position + worldOffsetDir * transform.localScale.magnitude;
        targettedTextGroup.transform.LookAt(lookPos);

        base.Update();
    }

    public override void Select()
    {
        EndingMenu.instance.VisitPlanet(endingMessage.message);
        AchievementSystem.instance.AcquireAchievement(endingMessage.endingNumber);
    }
}
