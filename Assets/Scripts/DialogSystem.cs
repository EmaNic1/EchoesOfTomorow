using System;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image portrait;

    DialogContainer currentDialog;
    int currentTextLine;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPrecent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    string lineToShow;

    NPCInteractable currentNpc;

    private void Start()
    {
        Show(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPrecent >= 1f) { return; }

        currentTime += Time.unscaledDeltaTime; // <-- pakeista
        visibleTextPrecent = currentTime / totalTimeToType;
        visibleTextPrecent = Mathf.Clamp(visibleTextPrecent, 0f, 1f);
        UpdateText();
    }

    void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPrecent);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    private void PushText()
    {
        if(visibleTextPrecent < 1f)
        {
            visibleTextPrecent = 1f;
            UpdateText();
            return;
        }
        if(currentTextLine >= currentDialog.line.Count)
        {
            Conclude();
        }
        else
        {
            CycleLine();
        }
    }

    void CycleLine()
    {
        lineToShow = currentDialog.line[currentTextLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        targetText.text = "";
        visibleTextPrecent = 0f;
        currentTextLine += 1;
    }

    public void Initialize(DialogContainer dialog, NPCInteractable npc)
    {
        currentNpc = npc;
        Show(true);
        currentDialog = dialog;
        currentTextLine = 0;
        CycleLine();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialog.actor.portrait;
        nameText.text = currentDialog.actor.name;
    }

    private void Show(bool v)
    {
        gameObject.SetActive(v);

        if (v)
            Time.timeScale = 0f; // sustabdo laiką
        else
            Time.timeScale = 1f; // leidžia laiką
    }

    private void Conclude()
    {
        Debug.Log("Dialog has ended.");

        if (currentNpc != null)
        {
            currentNpc.canTalk = false; // 🔒 UŽRAKINAM NPC
        }

        Show(false);

        // Specifinė Kamštuko logika
        if (currentDialog.actor.name == "Kamstukas")
        {
            GameManager.instance.selectProfession.uiPanel.SetActive(true);
        }

        currentNpc = null;
    }
}
