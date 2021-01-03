using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rollSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        transform.eulerAngles += Vector3.forward * rollSpeed * Time.deltaTime;
    }
}
