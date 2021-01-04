using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
    public Text nameText;
    public Text statusText;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetInfo(string name, bool completed)
    {
        nameText.text = name;
        statusText.text = completed ? "completed" : "";
    }

    public void EnableItem(bool enabled)
    {
        canvasGroup.alpha = enabled ? 1 : 0;
        canvasGroup.blocksRaycasts = enabled;
        canvasGroup.interactable = enabled;
    }
}
