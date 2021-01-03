using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Vector3 rotationAxis;
    public float rotationSpeed;

    protected void Update()
    {
        transform.RotateAround(transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
