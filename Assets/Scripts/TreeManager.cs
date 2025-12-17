using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : MonoBehaviour
{
    public TileMapTreeController treeManager;

    public void PickUp(Vector3Int position)
    {
        if(treeManager == null)
        {
            Debug.LogWarning("No tree manager are ref in the crops manager");
            return;
        }
        treeManager.PickUp(position);
    }

    internal bool Check(Vector3Int position)
    {
        if(treeManager == null)
        {
            Debug.LogWarning("No tree manager are ref in the crops manager");
            return false;
        }
        return treeManager.Check(position);
    }

    public void Plant(Vector3Int position, TreePlant toPlant)
    {
        if(treeManager == null)
        {
            Debug.LogWarning("No tree manager are ref in the crops manager");
            return;
        }
        treeManager.Plant(position, toPlant);
    }
}
