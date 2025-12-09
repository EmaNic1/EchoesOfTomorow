using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// valdo zemes busenas
/// </summary>

public class  CropTile
{
    public Crop crop;
    public int growStage;
    public int growTimer;
    public SpriteRenderer renderer;
    //public float damage;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if(crop == null)
            {
                return false;
            }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        //damage = 0;
    }
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    Tilemap TargerTilemap
    {
        get
        {
            if(targetTilemap == null)
            {
                GameObject go = GameObject.Find("CropsTileMap");
                if(go == null) {return null; }
                targetTilemap = go.GetComponent<Tilemap>();
            }
            return targetTilemap;
        }
    }
    
    [SerializeField] TileBase grassTile; // žolė medžiams
    [SerializeField] GameObject cropsSpritePrefab;

    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
    }

    // /// <summary>
    // /// pridetas
    // /// </summary>
    // /// <param name="map"></param>
    // public void SetTargetTilemap(Tilemap map)
    // {
    //     targetTilemap = map;
    // }

    public void Tick()
    {
        if(TargerTilemap == null){ return; }

        foreach(CropTile cropTile in crops.Values)
        {
            if (cropTile.crop == null)
            {
                continue;
            }

            //if(cropTile.growStage == 0)
            //{
            //targetTilemap.SetTile(cropTile.position, plowed);
            //}

            //cropTile.damage += 0.01f;

            //if(cropTile.damage > 1f)
            //{
            //cropTile.Harvested();
            //targetTilemap.SetTile(cropTile.position, plowed);
            //continue;
            //}

            if (cropTile.Complete)
            {
                Debug.Log("im done growing");
                continue;
            }

            cropTile.growTimer += 1;

            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                cropTile.growStage += 1;
            }
        }
    }

    /// <summary>
    /// Patikrina, ar nurodytoje pozicijoje jau egzistuoja įrašas
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool Check(Vector3Int position)
    {
        if(TargerTilemap == null){ return false; }

        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if(TargerTilemap == null){ return; }

        //Patikrina, ar šita plytelė jau yra crops žemėlapyje.
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }
        //jei ne, iškviečia CreatePlowedTile(position) – sukuria naują dirvos vietą.
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if(TargerTilemap == null){ return; }

        //Pakeičia isarta plytelę į „pasėtos“ (seeded) išvaizdą.
        TargerTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    /// <summary>
    /// sukuria isarta plytele
    /// </summary>
    /// <param name="position"></param>
    private void CreatePlowedTile(Vector3Int position)
    {
        if(TargerTilemap == null){ return; }

        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = TargerTilemap.CellToWorld(position);
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        TargerTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if(TargerTilemap == null){ return; }

        Vector2Int position = (Vector2Int)gridPosition;
        if(crops.ContainsKey(position) == false)
        {
            return;
        }
        CropTile cropTile = crops[position];

        if (cropTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(TargerTilemap.CellToWorld(gridPosition), cropTile.crop.yield, cropTile.crop.count);

            TargerTilemap.SetTile(gridPosition, plowed);

            cropTile.Harvested();
        }
    }
}
