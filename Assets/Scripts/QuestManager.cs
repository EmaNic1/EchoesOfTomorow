using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Questu valdymo klase (veliau)
/// </summary>

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Visos profesijų knygos")]
    public List<QuestBookData> allBooks;  // <- Čia pridedi visų knygų reference

    private void Awake()
    {
        Instance = this;
    }

    public void CompleteQuest(string questId)
    {
        foreach (var book in allBooks)
        {
            foreach (var quest in book.quests)
            {
                if (quest.questId == questId)
                {
                    quest.currentAmount = quest.requiredAmount;
                    Debug.Log($"Quest {questId} completed");
                    return;
                }
            }
        }
    }

    public void AddProgress(string questId, int amount = 1)
    {
        foreach (var book in allBooks)
        {
            foreach (var quest in book.quests)
            {
                if (quest.questId == questId && !quest.completed)
                {
                    quest.currentAmount += amount;

                    if (quest.completed)
                    {
                        Debug.Log($"Quest {questId} completed!");
                    }

                    return;
                }
            }
        }
    }

    public bool AreAllQuestsCompleted(QuestBookData book)
    {
        foreach (var quest in book.quests)
        {
            if (!quest.completed)
                return false;
        }
        return true;
    }
}
