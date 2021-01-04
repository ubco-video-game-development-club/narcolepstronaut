using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rollSpeed;

    private bool alive = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!alive) return;
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        transform.eulerAngles += Vector3.forward * rollSpeed * Time.deltaTime;
    }

    public void Die()
    {
        alive = false;
    }

    public bool IsAlive()
    {
        return alive;
    }
}
