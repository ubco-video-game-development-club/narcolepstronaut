using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    private const string PREF_ACHIEVEMENT_PREFIX = "HasAchievement";

    public static AchievementSystem instance;

    public Achievement[] achievements;

    private Dictionary<Achievement, bool> acquiredAchievements;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        acquiredAchievements = new Dictionary<Achievement, bool>();

        LoadAchievements();
    }

    public void AcquireAchievement(int endingNumber)
    {
        Achievement achievement = acquiredAchievements.Where(a => a.Key.endingNumber == endingNumber).Single().Key;
        acquiredAchievements[achievement] = true;
        SaveAchievements();
    }

    public Dictionary<Achievement, bool> GetAcquiredAchievements()
    {
        return acquiredAchievements;
    }

    private void SaveAchievements()
    {
        foreach (KeyValuePair<Achievement, bool> acquiredAchievement in acquiredAchievements)
        {
            int endingNumber = acquiredAchievement.Key.endingNumber;
            int hasAchievementBinary = acquiredAchievement.Value ? 1 : 0;
            PlayerPrefs.SetInt(PREF_ACHIEVEMENT_PREFIX + endingNumber, hasAchievementBinary);
        }
    }

    private void LoadAchievements()
    {
        foreach (Achievement achievement in achievements)
        {
            bool hasAchievement = PlayerPrefs.GetInt(PREF_ACHIEVEMENT_PREFIX + achievement.endingNumber) == 1;
            acquiredAchievements.Add(achievement, hasAchievement);
        }
    }
}
