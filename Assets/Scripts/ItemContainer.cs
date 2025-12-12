using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Laikomi inventoriaus objektai
/// </summary>

[Serializable]
//viena vieta objektui inventoriuje
public class ItemSlot
{
    public Items items;//objektas
    public int count;//kiek jo yra

    /// <summary>
    /// Kopijuoja
    /// </summary>
    /// <param name="slot"></param>
    public void Copy(ItemSlot slot)
    {
        items = slot.items;
        count = slot.count;
    }

    /// <summary>
    /// Nustaytyti reiksmes
    /// </summary>
    /// <param name="items"></param>
    /// <param name="count"></param>
    public void Set(Items items, int count)
    {
        this.items = items;
        this.count = count;
    }

    /// <summary>
    /// Isvalyti reiksmes
    /// </summary>
    public void Clear()
    {
        items = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]//duomenu failas
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slot = new List<ItemSlot>();//sarasas inventoriaus vietu
    public event Action OnInventoryChanged;//zinoti kada atsinaujina

    /// <summary>
    /// Prideda objekta i inventoriu
    /// </summary>
    /// <param name="items"></param>
    /// <param name="count"></param>
    public void Add(Items items, int count = 1)
    {
        if(items.stackable == true)
        {
            //ieskom ar objektas egzistuoja
            ItemSlot itemSlot = slot.Find(x => x.items == items);
            if(itemSlot != null)
            {
                //padidinam kieki
                itemSlot.count += count;
            }
            else
            {
                //jeigu neegzistuoja, surandame pirma tuscia vieta
                itemSlot = slot.Find(x => x.items == null);
                if(itemSlot != null)
                {
                    //idedame objekta
                    itemSlot.items = items;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //suranda pirma tuscia vieta
            ItemSlot itemSlot = slot.Find(x => x.items == null);
            if(itemSlot != null)
            {
                //idedame objekta
                itemSlot.items = items;
            }
        }

        NotifyChanged();
    }

    /// <summary>
    /// Pranesa apie pasikeitimus
    /// </summary>
    public void NotifyChanged()
    {
        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Panaikina objekta
    /// </summary>
    /// <param name="removeItem"></param>
    /// <param name="count"></param>
    public void RemoveItem(Items removeItem, int count = 1)
    {
        if (removeItem.stackable)
        {
            //randa vieta, kuroje objektas yra
            ItemSlot itemSlot = slot.Find(x => x.items == removeItem);
            //jeigu nerado
            if(itemSlot == null)
            {
                return;
            }
            //sumazina kieki
            itemSlot.count -= count;
            //jeigu rado
            if(itemSlot.count < 0)
            {
                //isvalo vieta
                itemSlot.Clear();
            }
        }
        else
        {
            while(count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slot.Find(x => x.items == removeItem);
                if(itemSlot == null)
                {
                    return;
                }
                itemSlot.Clear();
            }
        }
    }

    internal object CheckFreeSpace()
    {
        for(int i = 0; i < slot.Count; i++)
        {
            if(slot[i].items == null)
            {
                return true;
            }
        }

        return false;
    }

    internal bool CheckItem(ItemSlot checkItem)
    {
        ItemSlot itemSlot = slot.Find(x => x.items == checkItem.items);

        if (itemSlot == null) { return false; }

        if (checkItem.items.stackable)
        {
            return itemSlot.count > checkItem.count;
        }

        return true;
    }
}
