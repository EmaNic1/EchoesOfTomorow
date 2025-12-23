using UnityEngine;

public class ItemContainerPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slot[id]);
        Show(); // atvaizduoja inventorių ekrane
    }
    private void OnEnable() { inventory.OnInventoryChanged += Show; } 
    private void OnDisable() { inventory.OnInventoryChanged -= Show; }
}
