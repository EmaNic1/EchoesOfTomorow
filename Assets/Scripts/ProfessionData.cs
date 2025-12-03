using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class StartingItemEntry
{
    public Items item;
    public int amount = 1;
}

[CreateAssetMenu(menuName = "Data/Profession/Profession")]
public class ProfessionData : ScriptableObject
{
    public ProfessionTypes type;

    [Header("Pradžios daiktai")]
    public StartingItemEntry[] startingItems;

    [Header("Ilgalaikiai bonusai")]
    public int plantGrowFasterDays;
    public float cropBonusPercent;

    public int treeGrowFasterDays;
    public float woodBonusPercent;

    public bool unlockSpellBar;

    [Header("Knyga profesijai")]
    public GameObject professionBookPrefab;
}
