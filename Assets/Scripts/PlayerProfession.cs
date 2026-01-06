using UnityEngine;

public class PlayerProfession : MonoBehaviour
{
    public static PlayerProfession Instance;

    private void Awake()
    {
        Instance = this;
    }

    public ProfessionData currentProfession;
    public int level = 1;

    public void SetProfession(ProfessionData newProfession)
    {
        currentProfession = newProfession;
        level = 1;
        GiveStartingItems();
        //veliau sutavrkyti, kad knyga atsirastu namo viduje
        if (BookSpawner.Instance != null)
        {
            BookSpawner.Instance.SpawnProfessionBook(newProfession.professionBookPrefab);
        }
    }

    void GiveStartingItems()
    {
        if (currentProfession == null) return;

        foreach (var entry in currentProfession.startingItems)
        {
            GameManager.instance.inventoryContainer.Add(entry.item, entry.amount);
        }
    }

    // Naujas LevelUp metodas
    public void LevelUp()
    {
        if (level >= 2) return; // jau pasiektas maksimalus lygis

        level = 2;
        Debug.Log($"{currentProfession.type} pasiekė 2 lygį!");
    }

    public void TryLevelUp(QuestBookData book)
    {
        if (level >= 2)
            return;

        if (QuestManager.Instance.AreAllQuestsCompleted(book))
        {
            LevelUp();
        }
        else
        {
            Debug.Log("Dar neįvykdytos visos užduotys");
        }
    }

    public bool HasProfession(ProfessionTypes type)
    {
        return currentProfession != null && currentProfession.type == type;
    }

    // Pavyzdiniai bonus metodai
    public int GetTreeGrowFasterDays() => level >= 2 ? currentProfession.treeGrowFasterDays : 0;
    public float GetWoodBonusPercent() => level >= 2 ? currentProfession.woodBonusPercent : 0f;
}
