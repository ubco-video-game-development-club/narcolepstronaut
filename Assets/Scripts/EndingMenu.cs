using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMenu : MonoBehaviour
{
    public static EndingMenu instance;

    public Player player;
    public TMPro.TextMeshProUGUI textbox;

    public float initDelay = 0.5f;
    public float startDelay = 1.2f;
    public float charDelay = 0.07f;
    public float wordDelay = 0.1f;
    public float clearDelay = 4f;

    public AudioClip keyboardSound;

    private WaitForSeconds initDelayInstruction;
    private WaitForSeconds startDelayInstruction;
    private WaitForSeconds charDelayInstruction;
    private WaitForSeconds wordDelayInstruction;
    private WaitForSeconds clearDelayInstruction;

    private CanvasGroup hudGroup;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        initDelayInstruction = new WaitForSeconds(initDelay);
        startDelayInstruction = new WaitForSeconds(startDelay);
        charDelayInstruction = new WaitForSeconds(charDelay);
        wordDelayInstruction = new WaitForSeconds(wordDelay);
        clearDelayInstruction = new WaitForSeconds(clearDelay);

        hudGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEnding(string message)
    {
        if (!player.IsAlive()) return;
        player.Die();
        StartCoroutine(WriteTimedMessage(message));
    }

    private IEnumerator WriteTimedMessage(string message)
    {
        EnableHUDGroup(true);
        textbox.text = "";

        yield return initDelayInstruction;

        textbox.text = "> ";

        yield return startDelayInstruction;

        string[] words = message.Split(' ');
        foreach (string word in words)
        {
            char[] chars = word.ToCharArray();
            foreach (char c in chars)
            {
                textbox.text += c;
                audioSource.PlayOneShot(keyboardSound, 0.1f);
                yield return charDelayInstruction;
            }

            textbox.text += ' ';
            yield return wordDelayInstruction;
        }

        yield return clearDelayInstruction;

        // Load main menu
    }

    private void EnableHUDGroup(bool enabled)
    {
        hudGroup.alpha = enabled ? 1 : 0;
        hudGroup.blocksRaycasts = enabled;
        hudGroup.interactable = enabled;
    }
}
