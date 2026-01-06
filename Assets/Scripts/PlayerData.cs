using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(menuName ="Data/Player data")]
public class PlayerData : ScriptableObject
{
    public string characterName;
    public Gender characterGender;
    public int saveSlotId;

    public void ResetData()
    {
        characterName = "";
        characterGender = Gender.Female;
        saveSlotId = -1;
    }
}
