using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : Selectable
{
    public float wireCooldown = 2f;

    public AudioClip zapBejesus;
    public AudioClip zapDontTouch;
    public AudioClip zapGosh;
    public AudioClip zapOww;

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

        base.SetTargetted(targetted);
    }

    public override void Select()
    {
        if (onCooldown) return;
        
        int x = Random.Range(1,5);

        if (x == 1) audioSource.PlayOneShot(zapGosh);
        else if (x == 2) audioSource.PlayOneShot(zapDontTouch);
        else if (x == 3) audioSource.PlayOneShot(zapBejesus);
        else audioSource.PlayOneShot(zapOww);

        StartCoroutine(WireCooldown());

        base.Select();
    }

    private IEnumerator WireCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(wireCooldown);
        onCooldown = false;
    } 
}
