using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    ItemContainer targetItemContainer;
    InventoryController inventoryController;
    [SerializeField] ItemContainerPanel itemContainerPanel;
    Transform openedChest;
    [SerializeField] float maxDistance = 0.8f;

    void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    void Update()
    {
        if(openedChest != null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);
            if(distance > maxDistance)
            {
                openedChest.GetComponent<LootContainerInteract>().Close(GetComponent<Charater>());
            }
        }
    }

    public void Open(ItemContainer itemContainer, Transform _openedChest)
    {
        targetItemContainer = itemContainer;
        itemContainerPanel.inventory = targetItemContainer;
        inventoryController.Open();
        itemContainerPanel.gameObject.SetActive(true);
        openedChest = _openedChest;
    }

    public void Close()
    {
        inventoryController.Close();
        itemContainerPanel.gameObject.SetActive(false);
        openedChest = null;
    }
}
