using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Data/Profession/Profession")]
public class ProfessionData : ScriptableObject
{
    public ProfessionTypes type;

    [Header("Pradžios daiktai")]
    public Items[] startingItems;

    [Header("Ilgalaikiai bonusai")]
    public int plantGrowFasterDays;
    public float cropBonusPercent;

    public int treeGrowFasterDays;
    public float woodBonusPercent;
    public float rareTreeDropChance;

    public bool unlockSpellBar;
}
