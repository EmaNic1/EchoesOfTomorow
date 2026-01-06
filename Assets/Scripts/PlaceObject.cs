using UnityEngine;

[CreateAssetMenu(menuName ="Data/ToolAction/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Items item)
    {
        if(tileMapReadController.objecktManager.Check(gridPosition) == true)
        {
            return false;
        }

        tileMapReadController.objecktManager.Place(
            item,
            gridPosition
        );
        return true;
    }

    public override void OnItemUsed(Items usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
    }
}
