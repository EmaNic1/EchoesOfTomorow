using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : TimeAgent
{
    [SerializeField] TileBase plantedTile;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject spriteMark;

    Dictionary<Vector2Int, TreeTile> trees;

    private void Start()
    {
        trees = new Dictionary<Vector2Int, TreeTile>();
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
        foreach (TreeTile treeTile in trees.Values)
        {
            if (treeTile.tree == null) continue;
            if (treeTile.Complete) continue;

            treeTile.growTimer += 1;

            if (treeTile.growTimer >= treeTile.tree.growthStageTime[treeTile.growStage])
            {
                treeTile.renderer.gameObject.SetActive(true);

                // priskiriame naują sprite
                treeTile.renderer.sprite = treeTile.tree.sprites[treeTile.growStage];

                // --- ČIA ĮDEDAM SCALE AUGIMĄ ---
                // jei turime 3 augimo stadijas:
                // 0 → scale 1
                // 1 → scale 2
                // 2 → scale 3
                float scale = 1f + treeTile.growStage * 1f;
                treeTile.renderer.transform.localScale = new Vector3(scale, scale, 1f);

                // pereiname į kitą augimo stage
                treeTile.growStage += 1;
            }
        }
    }

    public void Plant(Vector3Int position, TreePlant toPlant)
    {
        if (trees.ContainsKey((Vector2Int)position)) return;

        TreeTile tree = new TreeTile();
        trees.Add((Vector2Int)position, tree);

        GameObject go = Instantiate(spriteMark);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.SetActive(false);
        tree.renderer = go.GetComponent<SpriteRenderer>();

        // Collider + TreeHit komponentai
        var collider = go.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0.02f, 0.3f);
        collider.size   = new Vector2(0.3f, 0.3f);
        var hit = go.AddComponent<TreeHit>();
        hit.tileData = tree;

        // pradinė scale (mažas medis)
        tree.renderer.transform.localScale = Vector3.one;

        tree.position = position;
        tree.tree = toPlant;

        targetTilemap.SetTile(position, plantedTile);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if (trees.ContainsKey(position) == false) return;

        TreeTile treeTile = trees[position];

        if (treeTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition),
                treeTile.tree.yield,
                treeTile.tree.count
            );

            treeTile.Harvested();
        }
    }
}
