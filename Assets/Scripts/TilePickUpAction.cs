using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Harvest")]
public class TilePickUpAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Items item)
    {
        tileMapReadController.cropsManager.PickUp(gridPosition);

        tileMapReadController.objecktManager.PickUp(gridPosition);

        return true;
    }
}
