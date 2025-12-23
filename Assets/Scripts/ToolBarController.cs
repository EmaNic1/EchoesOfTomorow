using System;
using UnityEngine;

/// <summary>
/// valdo tool bar
/// </summary>

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolBarSize = 9;
    [SerializeField] IconHighlithgt iconHighlithgt;
    int selectedTool;

    public Action<int> onChange;

    /// <summary>
    /// Paima objekta is inventoriaus, kuris yra pazymetas toolbar'e
    /// </summary>
    public Items GetItems
    {
        get
        {
            return GameManager.instance.inventoryContainer.slot[selectedTool].items;
        }
    }

    void Start()
    {
        onChange += UpdateHighlitghIcon;
        UpdateHighlitghIcon(selectedTool);
    }

    internal void Set(int id)
    { 
        selectedTool = id;
    }

    private void Update()
    {
        //leidzia zaidejui keisti pasirinkta daikta su scroll weel
        float delta = Input.mouseScrollDelta.y;
        if(Input.mouseScrollDelta.y != 0)
        {
            //pereina vienu i prieki
            if(delta > 0)
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolBarSize ? 0 : selectedTool);
            }
            //pereina vienu atgal
            else
            {
                selectedTool -= 1;
                selectedTool = (selectedTool <= 0 ? toolBarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    public void UpdateHighlitghIcon(int id)
    {
        Items items = GetItems;
        if(items == null)
        {
            iconHighlithgt.Show = false;
            return;
        }
        iconHighlithgt.Show = items.iconHighlight;
        if (items.iconHighlight)
        {
            iconHighlithgt.Set(items.icon);
        }
    }
}
