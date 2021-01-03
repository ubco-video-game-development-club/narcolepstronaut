using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : Planet
{
    public Planet orbitPlanet;
    public Vector3 orbitAxis;
    public float orbitSpeed;

    private Vector3 projectedOrbitAxis;

    void Start()
    {
        projectedOrbitAxis = Vector3.ProjectOnPlane(orbitAxis, transform.position - orbitPlanet.transform.position);
    }

    new void Update()
    {
        transform.RotateAround(orbitPlanet.transform.position, projectedOrbitAxis, orbitSpeed * Time.deltaTime);

        base.Update();
    }
}
