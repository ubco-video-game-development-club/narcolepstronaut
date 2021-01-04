using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookSelector : MonoBehaviour
{
    public static LookSelector instance;

    public float selectionRange;
    public float selectionRadius;
    public LayerMask selectionLayer;

    private Selectable target;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update()
    {
        // Update target
        bool didHit = Physics.SphereCast(
            transform.position,
            selectionRadius,
            transform.forward,
            out RaycastHit hitInfo,
            selectionRange,
            selectionLayer
        );

        if (!didHit || !hitInfo.transform.TryGetComponent<Selectable>(out target))
        {
            target = null;
        }

        // Handle selection input
        if (Input.GetKeyDown(KeyCode.E) && target != null)
        {
            target.Select();
        }
    }

    public Selectable GetTarget()
    {
        return target;
    }
}
