using UnityEngine;

/// <summary>
/// pasejama zeme
/// </summary>

[CreateAssetMenu(menuName ="Data/Tool Action/Seed tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Items item)
    {
        //Naudoja CropsManager.Check() metodą, kad patikrintų, ar toje vietoje jau yra išarta dirva.
        if (tileMapReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }

        //pakeičia plytelę į “seeded”
        tileMapReadController.cropsManager.Seed(gridPosition, item.crop);

        return true;
    }

    /// <summary>
    /// Panaikina sekla is inventoriaus
    /// </summary>
    /// <param name="usedItem"></param>
    /// <param name="inventory"></param>
    public override void OnItemUsed(Items usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
    }
}
