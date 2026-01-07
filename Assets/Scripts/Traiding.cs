using UnityEngine;

public class Traiding : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;

    [SerializeField] ItemPanel inventoryItemPanel;
    [SerializeField] ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;

    Store store;
    CurrencySystem money;

    void Awake()
    {
        money = GetComponent<CurrencySystem>();
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

    // =========================
    // PARDAVIMAS
    // =========================
    public void SellItem()
    {
        if (!GameManager.instance.dragAndDropController.CheckForSale())
            return;

        ItemSlot itemSlot = GameManager.instance.dragAndDropController.itemSlot;

        int pricePerItem = itemSlot.items.price;
        int amount = itemSlot.count;

        int moneyGain;

        // 🧮 MATEMATIKA
        if (itemSlot.items.stackable)
        {
            // price × kiekis
            moneyGain = pricePerItem * amount;
        }
        else
        {
            // vienas daiktas
            moneyGain = pricePerItem;
        }

        money.Add(moneyGain);
        itemSlot.Clear();
        GameManager.instance.dragAndDropController.UpdateIcon();

        QuestManager.Instance.AddProgress("GO_TO_SELLING", 1);
        QuestManager.Instance.AddProgress("GO_SELL", 1);
    }

    // =========================
    // PIRKIMAS
    // =========================
    public void BuyItem(int id)
    {
        Items itemToBuy = store.storeContent.slot[id].items;

        int amountToBuy = 1; // 🔢 kol kas visada 1
        int totalPrice = itemToBuy.price * amountToBuy;

        // 🧮 MATEMATIKA
        if (money.Check(totalPrice))
        {
            money.Decrease(totalPrice);
            playerInventory.Add(itemToBuy, amountToBuy);
            inventoryItemPanel.Show();
        }

        QuestManager.Instance.AddProgress("GO_TO_SHOPING", 1);
        QuestManager.Instance.AddProgress("GO_SHOPPING", 1);
    }
}
