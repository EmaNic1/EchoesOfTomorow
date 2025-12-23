using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
//apraso objekto duomenis(duomenu failai)
public class Items : ScriptableObject
{
    public string name;//objekto pavadinimas
    public bool stackable;//ar skaiciuojamas
    public Sprite icon;//objekto iconele
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public Crop crop;
    public TreePlant tree;
    public bool iconHighlight;
    public GameObject itemPrefab;

    public bool isWeapon;
    public int damage = 10;

    public int price = 50;
    public bool canBeSold = false;
}
