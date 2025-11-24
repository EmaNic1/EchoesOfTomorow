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
    }

    void GiveStartingItems()
    {
        if (currentProfession == null) return;

        foreach (var item in currentProfession.startingItems)
        {
            GameManager.instance.inventoryContainer.Add(item);
        }
    }

    public void LevelUp()
    {
        level = 2;
        Debug.Log("You reached level 2");
    }

    public bool HasProfession(ProfessionTypes type)
    {
        return currentProfession != null && currentProfession.type == type;
    }
}
