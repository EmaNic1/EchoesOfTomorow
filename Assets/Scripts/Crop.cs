using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    public int timeToGrow = 10;
    public Items yield;
    public int count = 1;

    public List<Sprite> sprites;
    public List<int> growthStageTime;
}
