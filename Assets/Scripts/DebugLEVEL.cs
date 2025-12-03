using UnityEngine;

public class DebugLEVEL : MonoBehaviour
{
    // Šis metodas bus pririštas prie UI mygtuko
    public void LevelUpPlayer()
    {
        if(PlayerProfession.Instance != null)
        {
            PlayerProfession.Instance.LevelUp();
        }
    }
}
