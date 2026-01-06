using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Water Tile", menuName = "Tiles/Water Tile")]
public class WaterTiles : Tile
{
    private void Reset() 
    { 
        colliderType = Tile.ColliderType.Grid; // arba Sprite, jei nori pagal formą 
    }
}
