using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.Search;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slot[id]);
        Show();
    }
}
