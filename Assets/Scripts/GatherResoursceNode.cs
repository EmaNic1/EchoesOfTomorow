using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// apibrezia resurus tipus
/// naudojamas kad toolAction zinotu kokius mazgus galima kirsti
/// </summary>
public enum ResourceNodeType
{
    Undifined,
    Tree,
    Stone

}

[CreateAssetMenu(menuName ="Data/Tool action/Gather Resource Node")]
//
public class GatherResoursceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    public override bool OnApply(Vector2 worldPoint)
    {

        // Sukuria kvadrato formos sritį („OverlapBoxAll“) ir surenka visus „Collider2D“ objektus toje srityje.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            worldPoint, new Vector2(sizeOfInteractableArea, sizeOfInteractableArea), 0f);

        // perziuri visus rastus objektus
        foreach (Collider2D c in colliders)
        {
            // Jei objektas turi „ToolHit“ komponentą, iškviečiamas „Hit()“ metodas.
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if(hit.CanBeHit(canHitNodesOfType) == true)
                {
                    hit.Hit();
                    return true;
                }

            }
        }
        return false;
    }
}
