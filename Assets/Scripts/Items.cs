using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Items : ScriptableObject
{
    public string name;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
}
