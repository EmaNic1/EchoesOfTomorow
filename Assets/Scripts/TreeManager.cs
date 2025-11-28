using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : TimeAgent
{
    [SerializeField] TileBase plantedTile;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject treeSpritePrefab;

    Dictionary<Vector2Int, TreeTile> trees;

    private void Start()
    {
        trees = new Dictionary<Vector2Int, TreeTile>();
        onTimeTick += Tick;
        Init();
    }

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
                treeTile.renderer.sprite = treeTile.tree.sprites[treeTile.growStage];
                treeTile.growStage += 1;
            }
        }
    }

    public void Plant(Vector3Int position, Tree toPlant)
    {
        if (trees.ContainsKey((Vector2Int)position)) return;

        TreeTile tree = new TreeTile();
        trees.Add((Vector2Int)position, tree);

        GameObject go = Instantiate(treeSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.SetActive(false);
        tree.renderer = go.GetComponent<SpriteRenderer>();

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
