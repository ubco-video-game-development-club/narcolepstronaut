using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaikaHUD : MonoBehaviour
{
    public static LaikaHUD instance;

    public TMPro.TextMeshProUGUI textbox;
    public Light holoLight; 

    public LaikaMessage[] messages;
    public float enableDelay = 0.3f;
    public float charDelay = 0.05f;
    public float wordDelay = 0.07f;
    public float clearDelay = 10f;
    public float deleteDelay = 0.05f;
    public float disableDelay = 0.5f;

    private int messageIndex = 0;
    private bool isBusy = false;
    private bool hudEnabled = false;

    private WaitForSeconds enableDelayInstruction;
    private WaitForSeconds charDelayInstruction;
    private WaitForSeconds wordDelayInstruction;
    private WaitForSeconds clearDelayInstruction;
    private WaitForSeconds deleteDelayInstruction;
    private WaitForSeconds disableDelayInstruction;

    private CanvasGroup hudGroup;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        enableDelayInstruction = new WaitForSeconds(enableDelay);
        charDelayInstruction = new WaitForSeconds(charDelay);
        wordDelayInstruction = new WaitForSeconds(wordDelay);
        clearDelayInstruction = new WaitForSeconds(clearDelay);
        deleteDelayInstruction = new WaitForSeconds(deleteDelay);
        disableDelayInstruction = new WaitForSeconds(disableDelay);

        hudGroup = GetComponent<CanvasGroup>();
    }

    public void UseScreen()
    {
        WriteRandomMessage();
    }

    public void EnableHUD()
    {
        holoLight.enabled = true;
        EnableHUDGroup(true);
        hudEnabled = true;
    }

    public void DisableHUD()
    {
        holoLight.enabled = false;
        EnableHUDGroup(false);
        hudEnabled = false;
    }

    public bool IsLaikaBusy()
    {
        return isBusy;
    }

    public float WriteRandomMessage()
    {
        if (isBusy) return 0;

        string message = messages[messageIndex].message;
        messageIndex = (messageIndex + 1) % messages.Length;

        return WriteMessage(message);
    }

    public float WriteMessage(string message)
    {
        if (isBusy) return 0;

        StopAllCoroutines();
        StartCoroutine(WriteTimedMessage(message));

        return messages[messageIndex].sleepinessBoost;
    }

    private IEnumerator WriteTimedMessage(string message)
    {
        isBusy = true;

        if (!hudEnabled)
        {
            EnableHUD();
            textbox.text = "> ";
            yield return enableDelayInstruction;
        }

        string[] words = message.Split(' ');
        foreach (string word in words)
        {
            char[] chars = word.ToCharArray();
            foreach (char c in chars)
            {
                textbox.text += c;
                yield return charDelayInstruction;
            }

            textbox.text += ' ';
            yield return wordDelayInstruction;
        }

        yield return clearDelayInstruction;

        int deleteLength = textbox.text.Length - 2;
        for (int i = 0; i < deleteLength; i++)
        {
            textbox.text = textbox.text.Substring(0, textbox.text.Length - 1);
            yield return deleteDelayInstruction;
        }

        yield return disableDelayInstruction;

        DisableHUD();
        isBusy = false;
    }

    private void EnableHUDGroup(bool enabled)
    {
        hudGroup.alpha = enabled ? 1 : 0;
        hudGroup.blocksRaycasts = enabled;
        hudGroup.interactable = enabled;
    }
}
