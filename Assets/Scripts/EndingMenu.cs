using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMenu : MonoBehaviour
{
    public static EndingMenu instance;

    public TMPro.TextMeshProUGUI textbox;

    public float initDelay = 0.5f;
    public float startDelay = 1.2f;
    public float charDelay = 0.07f;
    public float wordDelay = 0.1f;
    public float clearDelay = 4f;

    private WaitForSeconds initDelayInstruction;
    private WaitForSeconds startDelayInstruction;
    private WaitForSeconds charDelayInstruction;
    private WaitForSeconds wordDelayInstruction;
    private WaitForSeconds clearDelayInstruction;

    private CanvasGroup hudGroup;

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
    }

    public void VisitPlanet(string message)
    {
        StartCoroutine(WritePlanetMessage(message));
    }

    private IEnumerator WritePlanetMessage(string message)
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
