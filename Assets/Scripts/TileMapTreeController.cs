using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTreeController : TimeAgent
{
    [SerializeField] TreeContainer container;

    [SerializeField] TileBase plantedTile;
    Tilemap targetTilemap;

    [SerializeField] GameObject spriteMark;

    void Start()
    {
        GameManager.instance.GetComponent<TreeManager>().treeManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
        Init();
        RestoreRenderers();
    }

    private void RestoreRenderers()
    {
        for (int i = 0; i < container.trees.Count; i++)
        {
            var treeTile = container.trees[i];

            if (treeTile.renderer == null)
            {
                GameObject go = Instantiate(spriteMark, transform);
                go.transform.position = targetTilemap.CellToWorld(treeTile.position);
                go.transform.position -= Vector3.forward * 0.1f;
                treeTile.renderer = go.GetComponent<SpriteRenderer>();
            }

            // užtikrinam, kad collider visada būtų
            var collider = treeTile.renderer.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = treeTile.renderer.gameObject.AddComponent<BoxCollider2D>();
                collider.offset = new Vector2(0.02f, 0.3f);
                collider.size   = new Vector2(0.3f, 0.3f);
            }

            var hit = treeTile.renderer.GetComponent<TreeHit>();
            if (hit == null)
            {
                hit = treeTile.renderer.gameObject.AddComponent<TreeHit>();
                hit.tileData = treeTile;
            }

            if (treeTile.tree != null)
            {
                // naudok tą pačią logiką kaip crops: growStage - 1
                int stageIndex = Mathf.Clamp(treeTile.growStage - 1, 0, treeTile.tree.sprites.Count - 1);

                // jei medis pilnai užaugęs, rodom paskutinį sprite
                if (treeTile.Complete)
                {
                    stageIndex = treeTile.tree.sprites.Count - 1;
                }

                treeTile.renderer.sprite = treeTile.tree.sprites[stageIndex];
                treeTile.renderer.gameObject.SetActive(true);

                float scale = 1f + stageIndex * 1f;
                treeTile.renderer.transform.localScale = new Vector3(scale, scale, 1f);
            }
            else
            {
                treeTile.renderer.gameObject.SetActive(false);
            }
        }
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < container.trees.Count; i++)
        {
            VisualizeTile(container.trees[i]);
        }
    }

    public void VisualizeTile(TreeTile treeTile)
    {
        targetTilemap.SetTile(treeTile.position, treeTile.tree != null ? plantedTile : null);

        if (treeTile.renderer == null)
        {
            GameObject go = Instantiate(spriteMark, transform);
            go.transform.position = targetTilemap.CellToWorld(treeTile.position);
            go.transform.position -= Vector3.forward * 0.1f;
            treeTile.renderer = go.GetComponent<SpriteRenderer>();
        }

        bool growing = treeTile.tree != null && treeTile.growStage > 0;
        treeTile.renderer.gameObject.SetActive(false);

        if (growing)
        {
            int stageIndex = Mathf.Clamp(treeTile.growStage - 1, 0, treeTile.tree.sprites.Count - 1);

            if (treeTile.Complete)
            {
                stageIndex = treeTile.tree.sprites.Count - 1;
            }

            treeTile.renderer.sprite = treeTile.tree.sprites[stageIndex];
            treeTile.renderer.gameObject.SetActive(true);

            float scale = 1f + stageIndex * 1f;
            treeTile.renderer.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < container.trees.Count; i++)
        {
            if (container.trees[i].renderer != null)
            {
                Destroy(container.trees[i].renderer.gameObject);
                container.trees[i].renderer = null;
            }
        }
    }

    public void Tick()
    {
        foreach (TreeTile treeTile in container.trees)
        {
            if (treeTile.tree == null) continue;
            if (treeTile.Complete) continue;

            treeTile.growTimer += 1;

            if (treeTile.growTimer >= treeTile.tree.growthStageTime[treeTile.growStage])
            {
                treeTile.renderer.gameObject.SetActive(true);
                treeTile.renderer.sprite = treeTile.tree.sprites[treeTile.growStage];

                float scale = 1f + treeTile.growStage * 1f;
                treeTile.renderer.transform.localScale = new Vector3(scale, scale, 1f);

                if (treeTile.growStage < treeTile.tree.sprites.Count - 1)
                {
                    treeTile.growStage += 1;
                }
            }
        }
    }

    public void Plant(Vector3Int position, TreePlant toPlant)
    {
        // Jei medis jau pasodintas — nieko nedarom
        if (container.Get(position) != null)
        {
            return;
        }

        TreeTile tree = new TreeTile();
        container.Add(tree);

        GameObject go = Instantiate(spriteMark);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.SetActive(false);
        tree.renderer = go.GetComponent<SpriteRenderer>();

        var collider = go.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0.02f, 0.3f);
        collider.size   = new Vector2(0.3f, 0.3f);
        var hit = go.AddComponent<TreeHit>();
        hit.tileData = tree;

        tree.renderer.transform.localScale = Vector3.one;

        tree.position = position;
        tree.tree = toPlant;

        targetTilemap.SetTile(position, plantedTile);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        TreeTile tile = container.Get(gridPosition);
        if(tile == null){ return; }

        if (tile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition),
                tile.tree.yield,
                tile.tree.count
            );

            tile.Harvested();
            VisualizeTile(tile);
        }
    }

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }
}
