using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepinessSystem : MonoBehaviour
{
    public static SleepinessSystem instance;

    public Player player;

    // High sleepiness means you're more awake. Idk lol
    public float maxSleepiness = 200f;
    public float sleepinessRate = 10f;
    public float coffeeValue = 75f;
    public int minCoffeeUses = 5;
    public int maxCoffeeUses = 10;
    public float wirePokeValue = 100f;
    public float wireZapChance = 0.2f;
    public EndingMessage sleepEnding;
    public EndingMessage zappedEnding;

    private float sleepiness;
    private int totalCoffeeUses;
    private int numCoffeeUses;
    private bool isCoffeeAvailable = true;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        totalCoffeeUses = Random.Range(minCoffeeUses, maxCoffeeUses);
        sleepiness = maxSleepiness;
    }

    void Update()
    {
        sleepiness -= sleepinessRate * Time.deltaTime;

        if (sleepiness <= 0 && player.IsAlive())
        {
            EndingMenu.instance.PlayEnding(sleepEnding.message);
            AchievementSystem.instance.AcquireAchievement(sleepEnding.endingNumber);
        }
    }

    public bool IsCoffeeAvailable()
    {
        return isCoffeeAvailable;
    }

    public void DrinkCoffee()
    {
        if (!isCoffeeAvailable) return;

        sleepiness += coffeeValue;

        numCoffeeUses++;
        if (numCoffeeUses >= totalCoffeeUses)
        {
            isCoffeeAvailable = false;
        }
    }

    public void TouchWire()
    {
        if (Random.value < wireZapChance)
        {
            EndingMenu.instance.PlayEnding(zappedEnding.message);
            AchievementSystem.instance.AcquireAchievement(zappedEnding.endingNumber);
        }

        sleepiness += wirePokeValue;
    }
}
