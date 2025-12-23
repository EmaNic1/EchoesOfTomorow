using UnityEngine;

[CreateAssetMenu(menuName ="Data/ToolAction/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Items item)
    {

        tileMapReadController.objecktManager.Place(
            item,
            gridPosition
        );
        return true;
    }
}
