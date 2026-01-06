using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlacableObject
{
    public Items placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;

    public PlacableObject(Items items, Vector3Int pos)
    {
        placedItem = items;
        positionOnGrid = pos;
    }
}


[CreateAssetMenu(menuName ="Data/PlacableObject container")]
public class PlacableObjectsContainer : ScriptableObject
{
    public List<PlacableObject> placableObjects;

    internal PlacableObject Get(Vector3Int position)
    {
        return placableObjects.Find(x => x.positionOnGrid == position);
    }

    internal void Remove(PlacableObject placedObject)
    {
        placableObjects.Remove(placedObject);
    }
}
