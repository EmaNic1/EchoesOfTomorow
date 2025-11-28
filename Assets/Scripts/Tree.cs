using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tree")]
public class Tree : ScriptableObject
{
    public int timeToGrow = 10;
    public Items yield;
    public int count = 1;

    public List<Sprite> sprites;
    public List<int> growthStageTime;
}
