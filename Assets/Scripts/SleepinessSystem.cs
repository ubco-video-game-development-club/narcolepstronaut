using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SleepinessSystem : MonoBehaviour
{
    public static SleepinessSystem instance;

    public Player player;

    public AudioClip goodnight1;
    public AudioClip goodnight2;
    public AudioClip goodnight3;
    public AudioClip goodnight4;

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

    private AudioSource audioSource;

    private Vignette vignetteSettings;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            int x = Random.Range(1,5);

            if (x == 1) audioSource.PlayOneShot(goodnight1);
            else if (x == 2) audioSource.PlayOneShot(goodnight2);
            else if (x == 3) audioSource.PlayOneShot(goodnight3);
            else audioSource.PlayOneShot(goodnight4);
            
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
