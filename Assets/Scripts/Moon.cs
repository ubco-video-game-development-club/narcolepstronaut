using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : Planet
{
    public Planet orbitPlanet;
    public Vector3 orbitAxis;
    public float orbitSpeed;

    new void Update()
    {
        transform.RotateAround(orbitPlanet.transform.position, orbitAxis, orbitSpeed * Time.deltaTime);

        base.Update();
    }
}
