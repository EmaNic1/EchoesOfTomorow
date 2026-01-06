using UnityEngine;

public class BookInteractable : Interactable
{
    [SerializeField] QuestBookData bookData;

    public override void Interact(Charater charater)
    {
        if (QuestBookUI.Instance != null)
        {
            QuestBookUI.Instance.ShowBook(bookData);
        }
    }
}
