using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// valdo zymekli
/// </summary>

public class MarkerManager : MonoBehaviour
{
    [SerializeField] Tilemap targetTileMap;
    [SerializeField] TileBase tile;

    public Vector3Int markedCellPosition;
    Vector3Int oldCellPosition;
    bool show;

    private void Update()
    {
        if(show == false) { return; }
        targetTileMap.SetTile(oldCellPosition, null);//isvalo sena pazymeta vieta
        targetTileMap.SetTile(markedCellPosition, tile);//nustato nauja pazymeta vieta
        oldCellPosition = markedCellPosition;//issaugo kito kadro lyginimui
    }

    internal void Show(bool selected)
    {
        show = selected;
        targetTileMap.gameObject.SetActive(show);
    }
}
