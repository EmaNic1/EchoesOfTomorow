using System;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class Traiding : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;

    [SerializeField] ItemPanel inventoryItemPanel;

    Store store;

    CurrencySystem money;

    [SerializeField] ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;

    void Awake()
    {
        money = GetComponent<CurrencySystem>();
        //itemStorePanel = GetComponent<ItemStorePanel>();
    }

    public void BeginTraiding(Store store)
    {
        this.store = store;
        itemStorePanel.SetInventory(store.storeContent);
        Time.timeScale = 0f;
        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }

    public void StopTrading()
    {
        store = null;
        Time.timeScale = 1f;
        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
    

    public void SellItem()
    {
        if (GameManager.instance.dragAndDropController.CheckForSale() == true)
        {
            ItemSlot itemToSell = GameManager.instance.dragAndDropController.itemSlot;
            int moneyGain = itemToSell.items.stackable == true ? 
            itemToSell.items.price : 
            itemToSell.items.price;
            money.Add(moneyGain);
            itemToSell.Clear();
            GameManager.instance.dragAndDropController.UpdateIcon();
        }
    }

    internal void BuyItem(int id)
    {
        Items itemToBuy = store.storeContent.slot[id].items;
        int totalPrice = itemToBuy.price;

        if (money.Check(totalPrice) == true)
        {
            money.Decrease(totalPrice);
            playerInventory.Add(itemToBuy);
            inventoryItemPanel.Show();
        }
    }
}
