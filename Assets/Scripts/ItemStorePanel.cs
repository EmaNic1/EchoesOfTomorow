using System;
using NUnit.Framework;
using UnityEngine;

public class ItemStorePanel : ItemPanel
{
    [SerializeField] Traiding traiding;
    [SerializeField] AudioClip onOpenAudio;

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
        AudioManager.instance.Play(onOpenAudio);
    }

    private void SellItem()
    {
        traiding.SellItem();
        AudioManager.instance.Play(onOpenAudio);
    }


}
