using System;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolBarSize = 9;
    int selectedTool;

    public Action<int> onChange;

    public Items GetItems
    {
        get
        {
            return GameManager.instance.inventoryContainer.slot[selectedTool].items;
        }
    }

    internal void Set(int id)
    { 
        selectedTool = id;
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if(Input.mouseScrollDelta.y != 0)
        {
            if(delta > 0)
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolBarSize ? 0 : selectedTool);
            }
            else
            {
                selectedTool -= 1;
                selectedTool = (selectedTool <= 0 ? toolBarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }
}
