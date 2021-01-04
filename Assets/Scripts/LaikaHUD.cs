using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaikaHUD : MonoBehaviour
{
    public static LaikaHUD instance;

    public TMPro.TextMeshProUGUI textbox;
    public LaikaMessage[] messages;
    public float charDelay = 0.05f;
    public float wordDelay = 0.07f;
    public float clearDelay = 10f;
    public float deleteDelay = 0.05f;

    private WaitForSeconds charDelayInstruction;
    private WaitForSeconds wordDelayInstruction;
    private WaitForSeconds clearDelayInstruction;
    private WaitForSeconds deleteDelayInstruction;

    private int messageIndex = 0;
    private bool isBusy = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        charDelayInstruction = new WaitForSeconds(charDelay);
        wordDelayInstruction = new WaitForSeconds(wordDelay);
        clearDelayInstruction = new WaitForSeconds(clearDelay);
        deleteDelayInstruction = new WaitForSeconds(deleteDelay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBusy)
        {
            WriteRandomMessage();
        }
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
        textbox.text = "> ";

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

        isBusy = false;
    }
}
