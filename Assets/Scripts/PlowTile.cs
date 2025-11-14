using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// suaria plytele
/// </summary>

[CreateAssetMenu(menuName ="Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;

    /// <summary>
    /// vykdoma kai žaidėjas panaudoja įrankį ant Tilemap’o
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <param name="tileMapReadController"></param>
    /// <returns></returns>
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        //Naudoja TileMapReadController, kad gautų plytelę pagal tinklelio koordinatę.
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        //Tikrina, ar ta plytelė yra leidžiamų sąraše canPlow.
        if (canPlow.Contains(tileToPlow) == false)
        {
            return false;
        }
        // pakeicia plytelę į plowed
        tileMapReadController.cropsManager.Plow(gridPosition);

        return true;
    }
}
