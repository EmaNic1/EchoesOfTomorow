using UnityEngine;
using UnityEngine.Tilemaps;

public enum ZoneType
{
    Grassland,
    Forest,
    Plowed,
    Town
}

public class ZoneMapGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private TileBase[] biomeTiles; // [0]=vanduo, [1]=žolė
    [SerializeField] private TileBase dirtTile;     // ariama žemė

    [Header("Object Prefabs")]
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private GameObject[] rockPrefabs;
    [SerializeField] private GameObject[] ruinsPrefabs;
    [SerializeField] private float objectChance = 0.05f;
    [SerializeField] private float ruinsChance = 0.05f;

    [Header("Player House")]
    [SerializeField] private GameObject playerHousePrefab;
    [SerializeField] private Vector2Int playerHouseTilePos = new Vector2Int(42, 20);

    // [Header("Player Start Position")]
    // private Transform player;
    // [SerializeField] private Vector2Int playerStartTilePos = new Vector2Int(39, 17);

    [Header("Raudonas enemy 1")]
    [SerializeField] private GameObject enemy1;
    [SerializeField] private Vector2 enemy1StartTilePos1 = new Vector2(18.43f, 23.48f);

    [Header("Raudonas enemy 2")]
    [SerializeField] private GameObject enemy2;
    [SerializeField] private Vector2 enemy1StartTilePos2 = new Vector2(14.47f, 17.45f);

    [Header("Raudonas enemy 3")]
    [SerializeField] private GameObject enemy3;
    [SerializeField] private Vector2 enemy1StartTilePos3 = new Vector2(22.52f, 16.48f);

    [Header("Raudonas enemy 4")]
    [SerializeField] private GameObject enemy4;
    [SerializeField] private Vector2 enemy1StartTilePos4 = new Vector2(13.44f, 14.43f);

    [Header("Raudonas enemy 5")]
    [SerializeField] private GameObject enemy5;
    [SerializeField] private Vector2 enemy1StartTilePos5 = new Vector2(22.5f, 12.49f);

    

    [Header("Kamstukas")]
    [SerializeField] private GameObject kamstukasPrefab;
    [SerializeField] private Vector2Int kamstukasTilePos = new Vector2Int(42, 20);

    [Header("Darlo")]
    [SerializeField] private GameObject darloPrefab;
    [SerializeField] private Vector2Int darloTilePos = new Vector2Int(42, 20);

    [Header("Aegas")]
    [SerializeField] private GameObject aegasPrefab;
    [SerializeField] private Vector2Int aegasTilePos = new Vector2Int(42, 20);

    [Header("Mege")]
    [SerializeField] private GameObject megePrefab;
    [SerializeField] private Vector2Int megeTilePos = new Vector2Int(42, 20);

    [Header("Map Settings")]
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int mapHeight = 100;
    [SerializeField] private float noiseScale = 50f;
    [SerializeField] private int seed = 12345;

    private ZoneType[,] zoneMap;


    private void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;

        //Nustatoma seed
        Random.InitState(seed);
        //Sukuriamas zoneMap masyvas
        zoneMap = new ZoneType[mapWidth, mapHeight];
        //Sukuriamos zonos
        DefineZones();
        //Sugeneruojamas žemėlapis
        GenerateMap();
        SpawnPlayerHouse();
        SpawnKamstukas();
        SpawnDarlo();
        SpawnAegas();
        SpawnMege();

        PlayceEnemey1();
        PlayceEnemey2();
        PlayceEnemey3();
        PlayceEnemey4();
        PlayceEnemey5();

        
    }



    private void PlayceEnemey1()
    {
        // konvertuojam tile koordinatę į int
        Vector3Int cell = new Vector3Int(
            Mathf.FloorToInt(enemy1StartTilePos1.x),
            Mathf.FloorToInt(enemy1StartTilePos1.y),
            0
        );

        // gaunam world poziciją
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        // SPAWNINAM enemy prefabą scenoje
        Instantiate(enemy1, worldPos, Quaternion.identity);
    }


    private void PlayceEnemey2()
    {
         // konvertuojam tile koordinatę į int
        Vector3Int cell = new Vector3Int(
            Mathf.FloorToInt(enemy1StartTilePos2.x),
            Mathf.FloorToInt(enemy1StartTilePos2.y),
            0
        );

        // gaunam world poziciją
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        // SPAWNINAM enemy prefabą scenoje
        Instantiate(enemy2, worldPos, Quaternion.identity);
    }

    private void PlayceEnemey3()
    {
         // konvertuojam tile koordinatę į int
        Vector3Int cell = new Vector3Int(
            Mathf.FloorToInt(enemy1StartTilePos3.x),
            Mathf.FloorToInt(enemy1StartTilePos3.y),
            0
        );

        // gaunam world poziciją
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        // SPAWNINAM enemy prefabą scenoje
        Instantiate(enemy3, worldPos, Quaternion.identity);
    }

    private void PlayceEnemey4()
    {
        // konvertuojam tile koordinatę į int
        Vector3Int cell = new Vector3Int(
            Mathf.FloorToInt(enemy1StartTilePos4.x),
            Mathf.FloorToInt(enemy1StartTilePos4.y),
            0
        );

        // gaunam world poziciją
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        // SPAWNINAM enemy prefabą scenoje
        Instantiate(enemy4, worldPos, Quaternion.identity);
    }

    private void PlayceEnemey5()
    {
        // konvertuojam tile koordinatę į int
        Vector3Int cell = new Vector3Int(
            Mathf.FloorToInt(enemy1StartTilePos5.x),
            Mathf.FloorToInt(enemy1StartTilePos5.y),
            0
        );

        // gaunam world poziciją
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        // SPAWNINAM enemy prefabą scenoje
        Instantiate(enemy5, worldPos, Quaternion.identity);
    }


    private void SpawnKamstukas()
    {
        // nustatyta tile koordinatė
        Vector3Int cell = new Vector3Int(kamstukasTilePos.x, kamstukasTilePos.y, 0);

        // konvertuojama į world position
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        Instantiate(kamstukasPrefab, worldPos, Quaternion.identity);
    }

    private void SpawnDarlo()
    {
        // nustatyta tile koordinatė
        Vector3Int cell = new Vector3Int(darloTilePos.x, darloTilePos.y, 0);

        // konvertuojama į world position
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        Instantiate(darloPrefab, worldPos, Quaternion.identity);
    }

    private void SpawnAegas()
    {
        // nustatyta tile koordinatė
        Vector3Int cell = new Vector3Int(aegasTilePos.x, aegasTilePos.y, 0);

        // konvertuojama į world position
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        Instantiate(aegasPrefab, worldPos, Quaternion.identity);
    }

    private void SpawnMege()
    {
        // nustatyta tile koordinatė
        Vector3Int cell = new Vector3Int(megeTilePos.x, megeTilePos.y, 0);

        // konvertuojama į world position
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        Instantiate(megePrefab, worldPos, Quaternion.identity);
    }

    private void SpawnPlayerHouse()
    {
        // nustatyta tile koordinatė
        Vector3Int cell = new Vector3Int(playerHouseTilePos.x, playerHouseTilePos.y, 0);

        // konvertuojama į world position
        Vector3 worldPos = groundTilemap.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0);

        Instantiate(playerHousePrefab, worldPos, Quaternion.identity);
    }

    /// <summary>
    /// Sukuriamos zonos(misko zona, ariamos zemes zona, miesto zona)
    /// </summary>
    private void DefineZones()
    {

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                // Jei plytelė yra prie pat žemėlapio krašto – tai miškas
                if (x < 5 || y < 5 || x > mapWidth - 6 || y > mapHeight - 6)
                    zoneMap[x, y] = ZoneType.Forest;
                else
                    zoneMap[x, y] = ZoneType.Grassland;
            }
        }
        // Grassland visur pagal nutylėjimą
        //for (int x = 0; x < mapWidth; x++)
        //for (int y = 0; y < mapHeight; y++)
        //zoneMap[x, y] = ZoneType.Grassland;

        // Forest lopinėlis
        SetZone(10, 10, 20, 20, ZoneType.Forest);

        // Ariama žemė
        SetZone(33, 11, 5, 4, ZoneType.Plowed);

        // Town
        SetZone(60, 60, 15, 15, ZoneType.Town);
    }

    /// <summary>
    /// naudojamas nustatyti konkrečią stačiakampę zoną
    /// </summary>
    /// <param name="startX">nuo kur prasideda zonos kampas</param>
    /// <param name="startY">nuo kur prasideda zonos kampas</param>
    /// <param name="width">koks plotis</param>
    /// <param name="height">koks aukstis</param>
    /// <param name="type">kokia zona</param>
    private void SetZone(int startX, int startY, int width, int height, ZoneType type)
    {
        //Eina per visus stulpelius nuo startX iki startX + width
        for (int x = startX; x < startX + width && x < mapWidth; x++)
            //Eina per visus eilučių indeksus nuo startY iki startY + height
            for (int y = startY; y < startY + height && y < mapHeight; y++)
                //Kiekvienai plytelei tame stačiakampyje priskiriamas konkretus zonos tipas
                zoneMap[x, y] = type;
    }

    private void GenerateMap()
    {
        //sukuria mazus poslinkius pagal seed
        //naudojami perlin noice
        float seedOffsetX = seed * 0.01f;
        float seedOffsetY = seed * 0.01f + 100f;

        //dvigubas ciklas eina per kiekviena zemelapio plytele
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                TileBase tile = null;//kokia plytele bus dedama
                ZoneType zone = zoneMap[x, y];//paimama, kokiai zonai priklauso koordinatė

                //konvertuojama iš „Tilemap koordinačių“ į „pasaulio koordinates“
                Vector3 worldPos = groundTilemap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0.5f, 0.5f, 0);
                //CellToWorld() paverčia plytelės (x, y) indeksus į tikras pasaulio koordinates
                //new Vector3(0.5f, 0.5f, 0) šiek tiek pastumia poziciją į plytelės centrą

                switch (zone)
                {
                    //žolės tekstūra (biomeTiles[1]).
                    //Objektai: su tam tikra tikimybe(objectChance, pvz. 5 %) ant jos pastatomas medis arba akmuo.
                    //Random.value < 0.5f reiškia, kad 50 % tikimybė pasirinkti medį, 50 % – akmenį.
                    //Instantiate() sukuria tą objektą žaidimo scenoje.
                    case ZoneType.Forest:
                        tile = biomeTiles[1]; // žolė
                        if (Random.value < objectChance)
                        {
                            GameObject prefab = Random.value < 0.5f ?//galima pakeisti, kad butu pridedama daugiau medziu
                                treePrefabs[Random.Range(0, treePrefabs.Length)] :
                                rockPrefabs[Random.Range(0, rockPrefabs.Length)];
                            Instantiate(prefab, worldPos, Quaternion.identity, transform);
                        }
                        break;
                    
                    //dedama tik dirvos plytele, jokie kiti papildomi objektai nededami
                    case ZoneType.Plowed:
                        tile = dirtTile;
                        break;

                    //dedama tik zoles plytele, jokie kiti papildomi objektai nededami
                    case ZoneType.Town:
                        tile = biomeTiles[1]; // žolė
                        break;

                    //naudojamas Perlin noice
                    //Kuo mažesnis noise, tuo didesnė tikimybė, kad bus vanduo (biomeTiles[0]).
                    //Kuo didesnis noise, tuo didesnė tikimybė, kad bus žolė(biomeTiles[1]).
                    case ZoneType.Grassland:
                        float noise = Mathf.PerlinNoise((x / noiseScale) + seedOffsetX, (y / noiseScale) + seedOffsetY);
                        tile = noise < 0.3f ? biomeTiles[0] : biomeTiles[1];

                        // SPAWNINA RUINS TIK ANT ŽOLĖS (ne ant vandens)
                        if (tile == biomeTiles[1] && Random.value < ruinsChance)
                        {
                            GameObject ruin = ruinsPrefabs[Random.Range(0, ruinsPrefabs.Length)];
                            Instantiate(ruin, worldPos, Quaternion.identity, transform);
                        }
                        break;
                }

                groundTilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}