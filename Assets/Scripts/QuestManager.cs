using UnityEngine;

/// <summary>
/// Questu valdymo klase (veliau)
/// </summary>

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    //public NPCInteractable fisherman;//cia kaip pvz

    private void Awake()
    {
        Instance = this;
    }

    // Ateityje čia valdysime quest progresą
    public void OnQuestCompleted(string questId)
    {
        Debug.Log("Quest completed: " + questId);

        //Cia kai pvz
        //if (questId == "FISHING_INTRO")
        //{
            //fisherman.UnlockTalking();
        //}
    }
}
