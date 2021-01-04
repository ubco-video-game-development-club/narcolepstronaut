using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Selectable
{
    public CanvasGroup errorTextGroup;
    public float coffeeCooldown = 2f;

    public AudioClip badCoffee;
    public AudioClip coffee;
    public AudioClip grossCoffee;
    public AudioClip staleCoffee;

    private bool onCooldown = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    override protected void SetTargetted(bool targetted)
    {
        base.SetTargetted(false);

        if (onCooldown) return;

        if (!SleepinessSystem.instance.IsCoffeeAvailable())
        {
            textGroup = errorTextGroup;
            outline.color = 1;
        }

        base.SetTargetted(targetted);
    }

    public override void Select()
    {

        if (!SleepinessSystem.instance.IsCoffeeAvailable()) return; 
    
        if (onCooldown) return;

        int x = Random.Range(1,5);

        if (x == 1) audioSource.PlayOneShot(badCoffee);
        else if (x == 2) audioSource.PlayOneShot(coffee);
        else if (x == 3) audioSource.PlayOneShot(grossCoffee);
        else audioSource.PlayOneShot(staleCoffee);

        StartCoroutine(CoffeeCooldown());

        base.Select();
    }

    private IEnumerator CoffeeCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(coffeeCooldown);
        onCooldown = false;
    }
}
