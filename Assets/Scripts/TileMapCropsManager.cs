using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    Tilemap targetTilemap;

    [SerializeField] CropsContainer container;
    [SerializeField] TileBase grassTile; // žolė medžiams

    [SerializeField] GameObject cropsSpritePrefab;

    void Start()
    {
        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        VizualizeMap();
        Init();
        RestoreRenderers();
    }

    private void RestoreRenderers()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            var cropTile = container.crops[i];

            if (cropTile.renderer == null)
            {
                VisualizeTile(cropTile); // čia renderer sukuriamas ir sprite priskiriamas
            }
            else if (cropTile.crop != null)
            {
                int stageIndex = Mathf.Clamp(cropTile.growStage - 1, 0, cropTile.crop.sprites.Count - 1);
                cropTile.renderer.sprite = cropTile.crop.sprites[stageIndex]; // pirmiausia priskiriam sprite
                cropTile.renderer.gameObject.SetActive(true); // tada įjungiam GameObject
            }
            else
            {
                cropTile.renderer.gameObject.SetActive(false);
            }
        }
    }

    private void VizualizeMap()
    {
        for(int i = 0; i < container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }

    private void OnDestroy()
    {
        for(int i = 0; i < container.crops.Count; i++)
        {
            if(container.crops[i].renderer != null)
                Destroy(container.crops[i].renderer.gameObject);
        }
    }

    public void Tick()
    {
        if(targetTilemap == null){ return; }

        foreach(CropTile cropTile in container.crops)
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

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }

    public void Plow(Vector3Int position)
    {
        if(Check(position) == true) { return; }
        //jei ne, iškviečia CreatePlowedTile(position) – sukuria naują dirvos vietą.
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);

        if(tile == null) { return; }

        //Pakeičia isarta plytelę į „pasėtos“ (seeded) išvaizdą.
        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;
    }

    public void VisualizeTile(CropTile cropTile)
    {
        targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);

        if (cropTile.renderer == null)
        {
            GameObject go = Instantiate(cropsSpritePrefab, transform);
            go.transform.position = targetTilemap.CellToWorld(cropTile.position);
            go.transform.position -= Vector3.forward * 0.1f;
            cropTile.renderer = go.GetComponent<SpriteRenderer>();
        }

        bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];
        cropTile.renderer.gameObject.SetActive(false);
        if(growing == true)
        {
            cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage - 1];
        }
        
    }

    /// <summary>
    /// sukuria isarta plytele
    /// </summary>
    /// <param name="position"></param>
    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        container.Add(crop);

        crop.position = position;

        VisualizeTile(crop);

        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        CropTile tile = container.Get(gridPosition);
        if(tile == null) { return; }

        if (tile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), tile.crop.yield, tile.crop.count);

            tile.Harvested();
            VisualizeTile(tile);
        }
    }
}
