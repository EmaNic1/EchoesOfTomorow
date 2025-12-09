using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// peles pozicija pavercia i tile map koordinates
/// </summary>

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public CropsManager cropsManager;
    public TreeManager treeManager;

    /// <summary>
    /// peles koordinate is ekrano
    /// </summary>
    /// <param name="position"></param>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
    public Vector3Int GetGridBase(Vector2 position, bool mousePosition = false)
    {
        if(tilemap == null)
        {
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if(tilemap == null)
        {
            return Vector3Int.zero;
        }

        Vector3 worldPosition;

        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        //pavercia i pasaulio koordinate
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    /// <summary>
    /// grazina konkrecia plytele pagal pozicija
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public TileBase GetTileBase(Vector3Int gridPosition)
    {

        if(tilemap == null)
        {
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if(tilemap == null)
        {
            return null;
        }

        TileBase tile = tilemap.GetTile(gridPosition);

        return tile;
    }
}
