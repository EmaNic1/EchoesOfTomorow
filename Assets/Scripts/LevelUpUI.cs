using System.Collections;
using TMPro;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public static LevelUpUI Instance;

    public TMP_Text levelUpText;

    private void Awake()
    {
        Instance = this;

        if (levelUpText == null)
            Debug.LogError("LevelUpUI: levelUpText nėra priskirtas!");

        // Išjungiam visą panelą (t.y. šį GameObject)
        gameObject.SetActive(false);
    }

    public void ShowLevelUp(string message, float duration = 2f)
    {
        StopAllCoroutines();
        levelUpText.text = message;
        gameObject.SetActive(true); // įjungiam panel su tekstu
        StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false); // išjungiam panel
    }
}
