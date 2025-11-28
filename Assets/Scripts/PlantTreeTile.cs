using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Plant tree")]
public class PlantTreeTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Items item)
    {
        if (item.tree == null) return false;

        tileMapReadController.treeManager.Plant(gridPosition, item.tree);
        return true;
    }

    public override void OnItemUsed(Items usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
    }
}
