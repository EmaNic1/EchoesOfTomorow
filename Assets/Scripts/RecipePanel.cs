using UnityEngine;

public class RecipePanel : ItemPanel
{
    [SerializeField] RecipeList recipeList;
    [SerializeField] Crafting crafting;

    public override void Show()
    {
        if (recipeList == null || recipeList.recipes == null)
        {
            Debug.LogError("RecipeList arba recipes yra NULL", this);
            return;
        }

        if (buttons == null)
        {
            Debug.LogError("Buttons list yra NULL", this);
            return;
        }
        
        for(int i = 0; i < buttons.Count && i < recipeList.recipes.Count; i++)
        {
            buttons[i].Set(recipeList.recipes[i].output);
        }
    }

    public override void OnClick(int id)
    {
        if(id >= recipeList.recipes.Count){ return; }

        crafting.Craft(recipeList.recipes[id]);
    }
}
