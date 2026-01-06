using TMPro;
using UnityEngine;

public class QuestBookUI : MonoBehaviour
{
    public static QuestBookUI Instance;

    [Header("UI")]
    public GameObject rootPanel;
    public TMP_Text professionNameText;
    public Transform questListParent;
    public GameObject questTextPrefab;

    private void Awake()
    {
        Instance = this;
        rootPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        rootPanel.SetActive(!rootPanel.activeSelf);
    }

    private QuestBookData currentBook;

    public void ShowBook(QuestBookData data)
    {
        currentBook = data;
        rootPanel.SetActive(true);

        professionNameText.text = data.bookType.ToString();

        foreach (Transform child in questListParent)
            Destroy(child.gameObject);

        foreach (var quest in data.quests)
        {
            var go = Instantiate(questTextPrefab, questListParent);
            var text = go.GetComponent<TMP_Text>();

            text.text = quest.completed
                ? $"O {quest.description}"
                : $"X {quest.description}";
        }
    }

    public void OnLevelUpButton()
    {
        PlayerProfession.Instance.TryLevelUp(currentBook);

        // Tarkime, kad 2 lygis jau pasiektas
        if (PlayerProfession.Instance.level == 2)
        {
            LevelUpUI.Instance.ShowLevelUp("You reached level 2!");
        }
    }

    public void Close()
    {
        rootPanel.SetActive(false);
    } 
}
