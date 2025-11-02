using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Seed tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        if(tileMapReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Seed(gridPosition);

        return true;
    }

    public override void OnItemUsed(Items usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
    }
}
