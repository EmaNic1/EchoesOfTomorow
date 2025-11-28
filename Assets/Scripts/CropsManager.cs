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
    [SerializeField] TileBase grassTile; // žolė medžiams
    [SerializeField] GameObject cropsSpritePrefab;

    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
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

    public void Seed(Vector3Int position, Crop toSeed)
    {
        //Pakeičia isarta plytelę į „pasėtos“ (seeded) išvaizdą.
        targetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    /// <summary>
    /// sukuria isarta plytele
    /// </summary>
    /// <param name="position"></param>
    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if(crops.ContainsKey(position) == false)
        {
            return;
        }
        CropTile cropTile = crops[position];

        if (cropTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), cropTile.crop.yield, cropTile.crop.count);

            targetTilemap.SetTile(gridPosition, plowed);

            cropTile.Harvested();
        }
    }
}
