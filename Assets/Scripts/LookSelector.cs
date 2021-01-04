using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookSelector : MonoBehaviour
{
    public Renderer testBoi;
    public float selectionRange;
    public float selectionRadius;
    public LayerMask selectionLayer;
    public CanvasGroup selectionTextGroup;
    public Vector3 selectionTextOffset;

    void FixedUpdate()
    {
        RaycastHit hitInfo;
        bool didHit = Physics.SphereCast(transform.position, selectionRadius, transform.forward, out hitInfo, selectionRange, selectionLayer);
        if (didHit)
        {
            Vector3 hitPos = hitInfo.transform.position;

            // Selection text placement
            Vector3 textOffset = Vector3.Project(selectionTextOffset, transform.position - hitPos);
            Vector3 textPos = Camera.main.WorldToScreenPoint(hitPos + textOffset);
            EnableText(true);

            // TEST BOI
            testBoi.enabled = true;
            testBoi.transform.position = hitInfo.point;
        }
        else
        {
            EnableText(false);

            // TEST BOI
            testBoi.enabled = false;
        }
    }

    private void EnableText(bool enabled)
    {
        selectionTextGroup.alpha = enabled ? 1 : 0;
        selectionTextGroup.blocksRaycasts = enabled;
        selectionTextGroup.interactable = enabled;
    }
}
