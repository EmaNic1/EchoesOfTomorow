using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

/// <summary>
/// Inventoriaus langas
/// </summary>
public class InventoryPanel : ItemPanel
{
    [SerializeField] AudioClip onOpenAudio;

    /// <summary>
    /// Paspaudis ant objekto, ji iškviečia GameManager.instance.dragAndDropController.OnClick(...)
    /// </summary>
    /// <param name="id">objekto id</param>
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slot[id]);
        Show(); // atvaizduoja inventorių ekrane
    }

    private void OnEnable() 
    { 
        inventory.OnInventoryChanged += Show;        
        AudioManager.instance.Play(onOpenAudio);
    } 
    private void OnDisable() { inventory.OnInventoryChanged -= Show; }
}
