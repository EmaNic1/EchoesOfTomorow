using UnityEngine;

/// <summary>
/// bendra irankiu naudojimo klase
/// </summary>

public class ToolAction : ScriptableObject
{
    /// <summary>
    /// pasaulio koordinatės, kur įrankis bus naudojamas
    /// </summary>
    /// <param name="worldPoint"></param>
    /// <returns></returns>
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not impeleneted");
        return true;
    }

    /// <summary>
    /// plytelės koordinatės Tilemap.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <param name="tileMapReadController"></param>
    /// <returns></returns>
    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }

    /// <summary>
    /// metodas kviečiamas, kai įrankis arba daiktas turi būti sunaudotas.
    /// </summary>
    /// <param name="usedItem"></param>
    /// <param name="inventory"></param>
    public virtual void OnItemUsed(Items usedItem, ItemContainer inventory)
    {
        
    }
}
