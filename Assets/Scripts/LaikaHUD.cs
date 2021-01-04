using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaikaHUD : MonoBehaviour
{
    public static LaikaHUD instance;

    public TMPro.TextMeshProUGUI textbox;
    [TextArea]
    public string[] messages;
    public float charDelay = 0.05f;
    public float wordDelay = 0.07f;
    public float clearDelay = 4f;

    private WaitForSeconds charDelayInstruction;
    private WaitForSeconds wordDelayInstruction;
    private WaitForSeconds clearDelayInstruction;

    private int messageIndex = 0;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteRandomMessage();
        }
    }

    public void WriteRandomMessage()
    {
        string message = messages[messageIndex];
        WriteMessage(message);
        messageIndex = (messageIndex + 1) % messages.Length;
    }

    public void WriteMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(WriteTimedMessage(message));
    }

    private IEnumerator WriteTimedMessage(string message)
    {
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
        textbox.text = "> ";
    }
}
