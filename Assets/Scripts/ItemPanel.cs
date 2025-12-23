using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// tevine klase visiems ui elementams
/// </summary>

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;//conteinerio panel
    public List<InventoryButton> buttons;//mygtuku sarasas

    protected virtual void Start()
    {
        if(inventory == null){ return; }
        Init();
    }

    public void Init()
    {
        SetSourcePanel();
        SetIndex();//nustatomas mygtuko indeksas
        Show();//atvaizduojama inventoriaus busena
        inventory.OnInventoryChanged += Show;
    }

    private void SetSourcePanel()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetItemPanel(this);
        }
    }

    private void OnEnable()
    {
        inventory.OnInventoryChanged += Show;
    }

    private void OnDisable()
    {
        inventory.OnInventoryChanged -= Show;
    }


    // void OnEnable()
    // {
    //     Clear();
    //     Show();
    // }

    // private void OnDestroy()
    // {
    //     inventory.OnInventoryChanged -= Show;
    // }

    private void SetIndex()
    {
        //kiekvienam mygtukui suteikiamas indeksas
        for (int i = 0; i < inventory.slot.Count && i < buttons.Count; i++)
            buttons[i].SetIndex(i);
    }

    /// <summary>
    /// atvaizdavimo funkcija
    /// </summary>
    public virtual void Show()
    {
        if(inventory == null){ return; }

        //einame per visas pozicijas ir mygtukus
        for (int i = 0; i < buttons.Count && i < inventory.slot.Count; i++)
        {
            if (inventory.slot[i].items == null)
                buttons[i].Clean();
            else
                buttons[i].Set(inventory.slot[i]);

            buttons[i].Hihghlight(false);
        }

    }

    public void Clear()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clean();
        }
    }

    public void SetInventory(ItemContainer newInventory)
    {
        inventory = newInventory;
    }

    /// <summary>
    /// skirta perrasyti paveldetose klasese
    /// </summary>
    /// <param name="id"></param>
    public virtual void OnClick(int id) { }
}