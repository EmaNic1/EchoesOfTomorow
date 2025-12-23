using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlacableObject
{
    public Items placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;

    public PlacableObject(Items items, Transform target, Vector3Int pos)
    {
        placedItem = items;
        targetObject = target;
        positionOnGrid = pos;
    }
}


[CreateAssetMenu(menuName ="Data/PlacableObject container")]
public class PlacableObjectsContainer : ScriptableObject
{
    public List<PlacableObject> placableObjects;
}
