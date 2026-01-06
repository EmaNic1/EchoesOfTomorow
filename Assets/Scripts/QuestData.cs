using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string questId;
    public string description;

    public int requiredAmount;
    public int currentAmount;

    public bool completed => currentAmount >= requiredAmount;
}
