using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] private ItemSlot itemSlot;
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private ItemContainer inventory;

    private RectTransform iconTransform;
    private Image itemIconImage;

    private void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                ItemSpawnManager.instance.SpawnItem(worldPosition, itemSlot.items, itemSlot.count);

                itemSlot.Clear();
                itemIcon.SetActive(false);

                inventory?.NotifyChanged(); // <-- event kviečiamas po item spawn
            }
        }
    }

    internal void OnClick(ItemSlot slotToSwap)
    {
        if (itemSlot.items == null)
        {
            itemSlot.Copy(slotToSwap);
            slotToSwap.Clear();
        }
        else
        {
            Items tempItems = slotToSwap.items;
            int tempCount = slotToSwap.count;

            slotToSwap.Copy(itemSlot);
            itemSlot.Set(tempItems, tempCount);
        }

        UpdateIcon();
        inventory?.NotifyChanged(); // <-- event kviečiamas po swap
    }

    private void UpdateIcon()
    {
        if (itemSlot.items == null)
        {
            itemIcon.SetActive(false);
        }
        else
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.items.icon;
        }
    }
}