
using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacabelObjectManager : MonoBehaviour
{
    [SerializeField] PlacableObjectsContainer placableObjects;
    [SerializeField] Tilemap targetTilemap;

    void Start()
    {
        GameManager.instance.GetComponent<PlacableObjectRef>().placabelObjectManager = this;
        VisualizeMap();
    }

    void OnDestroy()
    {
        for(int i = 0; i < placableObjects.placableObjects.Count; i++)
        {
            placableObjects.placableObjects[i].targetObject = null;
        }
    }

    private void VisualizeMap()
    {
        for(int i = 0; i < placableObjects.placableObjects.Count; i++)
        {
            VisualizeItem(placableObjects.placableObjects[i]);
        }
    }

    private void VisualizeItem(PlacableObject placableObject)
    {
        GameObject go = Instantiate(placableObject.placedItem.itemPrefab);
        Vector3 position = targetTilemap.CellToWorld(placableObject.positionOnGrid) + targetTilemap.cellSize/2;
        go.transform.position = position;

        placableObject.targetObject = go.transform;
    }

    public bool Check(Vector3Int position)
    {
        return placableObjects.Get(position) != null;
    }

    public void Place(Items items, Vector3Int positionOnGrid)
    {
        if(Check(positionOnGrid) == true){ return ; }
        PlacableObject placableObject = new PlacableObject(items, positionOnGrid);
        VisualizeItem(placableObject);
        placableObjects.placableObjects.Add(placableObject);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        PlacableObject placedObject = placableObjects.Get(gridPosition);

        if(placedObject == null)
        {
            return;
        }

        ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), placedObject.placedItem, 1);

        Destroy(placedObject.targetObject.gameObject);

        placableObjects.Remove(placedObject);
    }
}
