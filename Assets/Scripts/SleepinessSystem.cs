using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SleepinessSystem : MonoBehaviour
{
    public static SleepinessSystem instance;

    public Player player;

    // High sleepiness means you're more awake. Idk lol
    public float maxSleepiness = 200f;
    public float sleepinessRate = 4f;
    public float coffeeValue = 75f;
    public int minCoffeeUses = 5;
    public int maxCoffeeUses = 10;
    public float wirePokeValue = 100f;
    public float wireZapChance = 0.2f;
    public float minIntensity = 0.2f;
    public float maxIntensity = 1f;
    public float noiseScale = 10f;
    public float noiseRate = 0.1f;
    public EndingMessage sleepEnding;
    public EndingMessage zappedEnding;

    private float sleepiness;
    private int totalCoffeeUses;
    private int numCoffeeUses;
    private bool isCoffeeAvailable = true;

    private Vignette vignetteSettings;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        vignetteSettings = Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();

        totalCoffeeUses = Random.Range(minCoffeeUses, maxCoffeeUses);
        sleepiness = maxSleepiness;
    }

    void Update()
    {
        sleepiness -= sleepinessRate * Time.deltaTime;

        // Apply visual eyes closing effect
        if (sleepiness < maxSleepiness / 2)
        {
            float noise = Mathf.PerlinNoise(sleepiness * noiseRate, sleepiness * noiseRate) * noiseScale;
            float currentIntensity = ((maxSleepiness / 2) - sleepiness + noise) / (maxSleepiness / 2 + noiseScale);
            vignetteSettings.intensity.value = Mathf.Lerp(minIntensity, maxIntensity, currentIntensity);
        }
        
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
