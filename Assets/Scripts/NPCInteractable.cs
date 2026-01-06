using UnityEngine;

public class NPCInteractable : Interactable
{
    [SerializeField] DialogContainer dialog;

    public bool canTalk = true;  // <-- ateityje valdys Quests/Progress

    public override void Interact(Charater charater)
    {
        if (!canTalk)
        {
            Debug.Log("NPC nenori/negali dabar kalbėti.");
            return;
        }

        GameManager.instance.dialogSystem.Initialize(dialog, this);
        //canTalk = false; // <-- dabar tik vieną kartą per ciklą
    }

    public void UnlockTalking()
    {
        canTalk = true;
    }
}
