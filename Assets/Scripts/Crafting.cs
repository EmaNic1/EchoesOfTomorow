using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;

    public void Craft(CraftingRecipe recipe)
    {
        if (!inventory.CheckFreeSpace())
        {
            Debug.Log("Not enough space in inventory");
            return;
        }

        for(int i = 0; i < recipe.elements.Count; i++)
        {
            if (!inventory.CheckItem(recipe.elements[i]))
            {
                Debug.Log("Crafting elements are not present");
                return;
            }
        }

        for(int i = 0; i < recipe.elements.Count; i++)
        {
            inventory.RemoveItem(recipe.elements[i].items, recipe.elements[i].count);
        }

        inventory.Add(recipe.output.items, recipe.output.count);
    }


    // public void Craft(CraftingRecipe recipe)
    // {
    //     Debug.Log("Not enough space in inventory");
    //     if (inventory.CheckFreeSpace() == null)
    //     {
    //         return;
    //     }

    //     bool craftable = true;
    //     for(int i = 0; i < recipe.elements.Count; i++)
    //     {
    //         if (inventory.CheckItem(recipe.elements[i]) == false)
    //         {
    //             //craftable = false;
    //             Debug.Log("Crafting elements are not present");
    //             //break;
    //             return;
    //         }
    //     }

    //     //if(craftable == false){ return; }

    //     for(int i = 0; i < recipe.elements.Count; i++)
    //     {
    //         inventory.RemoveItem(recipe.elements[i].items, recipe.elements[i].count);
    //     }

    //     inventory.Add(recipe.output.items, recipe.output.count);
    // }
}
