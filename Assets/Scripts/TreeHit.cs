using UnityEngine;
using System.Collections.Generic;

public class TreeHit : ToolHit
{
    public TreeTile tileData; // kur saugome TreeTile iš TreeManager
    public ResourceNodeType nodeType = ResourceNodeType.Tree;

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

    public override void Hit()
    {
        if (tileData == null) return;

        Debug.Log($"Hit: growTimer={tileData.growTimer}, timeToGrow={tileData.tree.timeToGrow}, Complete={tileData.Complete}");

        if (tileData.Complete)
        {
            Debug.Log("Spawning item: " + tileData.tree.yield.name);
            ItemSpawnManager.instance.SpawnItem(
                tileData.renderer.transform.position,
                tileData.tree.yield,
                tileData.tree.count
            );

            tileData.Harvested();
        }
    }
}
