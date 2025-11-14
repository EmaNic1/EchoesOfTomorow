using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// valdo zemes busenas
/// </summary>

public class  Crops
{
    
}

public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    Dictionary<Vector2Int, Crops> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crops>();
    }

    /// <summary>
    /// Patikrina, ar nurodytoje pozicijoje jau egzistuoja įrašas
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        //Patikrina, ar šita plytelė jau yra crops žemėlapyje.
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }
        //jei ne, iškviečia CreatePlowedTile(position) – sukuria naują dirvos vietą.
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position)
    {
        //Pakeičia isarta plytelę į „pasėtos“ (seeded) išvaizdą.
        targetTilemap.SetTile(position, seeded);
    }

    /// <summary>
    /// sukuria isarta plytele
    /// </summary>
    /// <param name="position"></param>
    private void CreatePlowedTile(Vector3Int position)
    {
        Crops crop = new Crops();
        crops.Add((Vector2Int)position, crop);

        targetTilemap.SetTile(position, plowed);
    }
}
