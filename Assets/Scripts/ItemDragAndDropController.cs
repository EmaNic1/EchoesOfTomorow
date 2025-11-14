using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Atlieka veiksmus kaip paimti objekta ir ji perkeliati i kita inventoriaus vieta
/// </summary>

public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] private ItemSlot itemSlot;//saugo paimta objekta
    [SerializeField] private GameObject itemIcon;//rodoma objekto iconole
    [SerializeField] private ItemContainer inventory;//nuoroda i inventoriu

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
        //kol objekto iconole yra laikoma
        if (itemIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;

            //jei paspaudziam pele mygtuka inventoriuje, o ne uz inventoriaus
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //atnaujina objekto vieta
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                ItemSpawnManager.instance.SpawnItem(worldPosition, itemSlot.items, itemSlot.count);

                itemSlot.Clear();
                itemIcon.SetActive(false);

                inventory?.NotifyChanged(); // event kviečiamas po item spawn
            }
        }
    }

    /// <summary>
    /// Pakeitimas vietomis "swap mechanizmas"
    /// </summary>
    /// <param name="slotToSwap"></param>
    internal void OnClick(ItemSlot slotToSwap)
    {
        //jeigu joks objektas nebuvo paimtas pakeiciam su tuscia vieta
        if (itemSlot.items == null)
        {
            itemSlot.Copy(slotToSwap);
            slotToSwap.Clear();
        }
        //jeigu objektas buvo paimtas, sukeiciam su paimtu objektu
        else
        {
            Items tempItems = slotToSwap.items;
            int tempCount = slotToSwap.count;

            slotToSwap.Copy(itemSlot);
            itemSlot.Set(tempItems, tempCount);
        }

        UpdateIcon();
        inventory?.NotifyChanged(); // pranesa apie pasikeitimus
    }

    /// <summary>
    /// Atnaujina objekto iconele(ta kuri yra po pelyte)
    /// </summary>
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