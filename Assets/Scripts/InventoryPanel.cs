using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.Search;
using UnityEngine;

/// <summary>
/// Inventoriaus langas
/// </summary>

public class InventoryPanel : ItemPanel
{
    /// <summary>
    /// Paspaudis ant objekto, ji iškviečia GameManager.instance.dragAndDropController.OnClick(...)
    /// </summary>
    /// <param name="id">objekto id</param>
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slot[id]);
        Show();//atvaizduoja inventoriu ekrane
    }
}
