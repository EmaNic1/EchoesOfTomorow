using System;
using NUnit.Framework;
using UnityEngine;

public class ItemStorePanel : ItemPanel
{
    [SerializeField] Traiding traiding;

    public override void OnClick(int id)
    {
        if(GameManager.instance.dragAndDropController.itemSlot.items == null)
        {
            BuyItem(id);
        }
        else
        {
            SellItem();
        }

        Show();
    }

    private void BuyItem(int id)
    {
        traiding.BuyItem(id);
    }

    private void SellItem()
    {
        traiding.SellItem();
    }


}
