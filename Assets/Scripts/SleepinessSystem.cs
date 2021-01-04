using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepinessSystem : MonoBehaviour
{
    // High sleepiness means you're more awake. Idk lol
    public float maxSleepiness = 200;
    public float coffeeValue = 50;

    private float sleepiness;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DrinkCoffee()
    {
        sleepiness += 50;
    }
}
