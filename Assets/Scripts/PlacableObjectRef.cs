using System;
using UnityEngine;

public class PlacableObjectRef : MonoBehaviour
{
    public PlacabelObjectManager placabelObjectManager;

    public void Place(Items items, Vector3Int pos)
    {
        if(placabelObjectManager == null)
        {
            Debug.LogWarning("No placableobjects manager ref");
            return;
        }

        placabelObjectManager.Place(items, pos);
    }

    public bool Check(Vector3Int pos)
    {
        if(placabelObjectManager == null)
        {
            Debug.LogWarning("No placableobjects manager ref");
            return false;
        }

        return placabelObjectManager.Check(pos);
        
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if(placabelObjectManager == null)
        {
            Debug.LogWarning("No placableobjects manager ref");
            return ;
        }

        placabelObjectManager.PickUp(gridPosition);
    }
}
