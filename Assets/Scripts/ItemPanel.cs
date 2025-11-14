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
        Init();
    }

    public void Init()
    {
        SetIndex();//nustatomas mygtuko indeksas
        Show();//atvaizduojama inventoriaus busena
        inventory.OnInventoryChanged += Show;
    }

    private void OnDestroy()
    {
        inventory.OnInventoryChanged -= Show;
    }

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
        //einame per visas pozicijas ir mygtukus
        for (int i = 0; i < inventory.slot.Count && i < buttons.Count; i++)
        {
            //jei vieta tuscia, mygtukas isvalomas
            if (inventory.slot[i].items == null)
                buttons[i].Clean();
            //kita atveju mygtuko duomenis atnaujina
            else
                buttons[i].Set(inventory.slot[i]);

            // Išjungiam highlight visiems
            buttons[i].Hihghlight(false);
        }
    }

    /// <summary>
    /// skirta perrasyti paveldetose klasese
    /// </summary>
    /// <param name="id"></param>
    public virtual void OnClick(int id) { }
}