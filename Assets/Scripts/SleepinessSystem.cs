using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepinessSystem : MonoBehaviour
{
    public static SleepinessSystem instance;

    // High sleepiness means you're more awake. Idk lol
    public float maxSleepiness = 200f;
    public float coffeeValue = 50f;
    public int minCoffeeUses = 3;
    public int maxCoffeeUses = 6;
    public float wirePokeValue = 150f;
    public float wireZapChance = 0.2f;

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
            Debug.Log("Zapped");
        }

        sleepiness += wirePokeValue;
    }
}
