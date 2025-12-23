
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacabelObjectManager : MonoBehaviour
{
    [SerializeField] PlacableObjectsContainer placableObjects;
    [SerializeField] Tilemap targetTilemap;

    void Start()
    {
        GameManager.instance.GetComponent<PlacableObjectRef>().placabelObjectManager = this;
    }

    public void Place(Items items, Vector3Int positionOnGrid)
    {
        GameObject go = Instantiate(items.itemPrefab);
        Vector3 position = targetTilemap.CellToWorld(positionOnGrid) + targetTilemap.cellSize/2;
        go.transform.position = position;
        placableObjects.placableObjects.Add(new PlacableObject(items, go.transform, positionOnGrid));
    }
}
