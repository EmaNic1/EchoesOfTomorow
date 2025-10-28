using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    protected virtual void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
        inventory.OnInventoryChanged += Show;
    }

    private void OnDestroy()
    {
        inventory.OnInventoryChanged -= Show;
    }

    private void SetIndex()
    {
        for (int i = 0; i < inventory.slot.Count && i < buttons.Count; i++)
            buttons[i].SetIndex(i);
    }

    public virtual void Show()
    {
        for (int i = 0; i < inventory.slot.Count && i < buttons.Count; i++)
        {
            if (inventory.slot[i].items == null)
                buttons[i].Clean();
            else
                buttons[i].Set(inventory.slot[i]);

            // Išjungiam highlight visiems
            buttons[i].Hihghlight(false);
        }
    }

    public virtual void OnClick(int id) { }
}