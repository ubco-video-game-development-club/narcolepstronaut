using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMenu : MonoBehaviour
{
    public AchievementItem achievementItemPrefab;
    public Transform achievementItemParent;
    public Button leftButton;
    public Button rightButton;
    public Text pageText;
    public int maxPerPage = 6;
    public int verticalGap = 30;

    private AchievementItem[] achievementItems;
    private int page = 0;
    private bool menuEnabled = false;

    private CanvasGroup menuGroup;

    void Awake()
    {
        menuGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        achievementItems = new AchievementItem[maxPerPage];

        float spawnY = 0;
        for (int i = 0; i < maxPerPage; i++)
        {
            AchievementItem achievementItem = Instantiate(achievementItemPrefab, achievementItemParent);
            achievementItem.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, spawnY);
            spawnY -= verticalGap;
            achievementItems[i] = achievementItem;
        }

        UpdatePage();
    }

    public void ToggleMenu()
    {
        menuEnabled = !menuEnabled;
        EnableMenu(menuEnabled);
    }

    public void NextPage()
    {
        page++;
        UpdatePage();
    }

    public void PrevPage()
    {
        page--;
        UpdatePage();
    }

    private void UpdatePage()
    {
        pageText.text = "" + (page + 1);
        leftButton.interactable = CanMovePrev();
        rightButton.interactable = CanMoveNext();

        for (int i = 0; i < achievementItems.Length; i++)
        {
            achievementItems[i].EnableItem(false);
        }

        int startIndex = page * maxPerPage;
        for (int i = 0; i < ItemsOnPage(); i++)
        {
            Achievement achievement = AchievementSystem.instance.achievements[startIndex + i];
            bool completed = AchievementSystem.instance.IsAcquired(achievement);
            achievementItems[i].SetInfo(achievement.title, completed);
            achievementItems[i].EnableItem(true);
        }
    }

    private int ItemsOnPage()
    {
        return Mathf.Clamp(AchievementSystem.instance.achievements.Length - page * maxPerPage, 1, 6);
    }

    private bool CanMoveNext()
    {
        return AchievementSystem.instance.achievements.Length - (page + 1) * maxPerPage > 0;
    }

    private bool CanMovePrev()
    {
        return page > 0;
    }

    public void EnableMenu(bool enabled)
    {
        menuGroup.alpha = enabled ? 1 : 0;
        menuGroup.blocksRaycasts = enabled;
        menuGroup.interactable = enabled;
    }
}
