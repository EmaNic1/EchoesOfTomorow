using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Book/Quest Book")]
public class QuestBookData : ScriptableObject
{
    public BookType bookType;
    public List<QuestData> quests;
}
