using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Items items;
    public int count;

    public void Copy(ItemSlot slot)
    {
        items = slot.items;
        count = slot.count;
    }

    public void Set(Items items, int count)
    {
        this.items = items;
        this.count = count;
    }

    public void Clear()
    {
        items = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slot;
    public event Action OnInventoryChanged;

    public void Add(Items items, int count = 1)
    {
        if(items.stackable == true)
        {
            ItemSlot itemSlot = slot.Find(x => x.items == items);
            if(itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slot.Find(x => x.items == null);
                if(itemSlot != null)
                {
                    itemSlot.items = items;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //non stackable item to ours item container
            ItemSlot itemSlot = slot.Find(x => x.items == null);
            if(itemSlot != null)
            {
                itemSlot.items = items;
            }
        }

        NotifyChanged();
    }

    public void NotifyChanged()
    {
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(Items removeItem, int count = 1)
    {
        if (removeItem.stackable)
        {
            ItemSlot itemSlot = slot.Find(x => x.items == removeItem);
            if(itemSlot == null)
            {
                return;
            }
            itemSlot.count -= count;
            if(itemSlot.count < 0)
            {
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
}
