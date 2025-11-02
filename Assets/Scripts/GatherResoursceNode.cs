using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum ResourceNodeType
{
    Undifined,
    Tree,
    Stone

}

[CreateAssetMenu(menuName ="Data/Tool action/Gather Resource Node")]
public class GatherResoursceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    public override bool OnApply(Vector2 worldPoint)
    {

        // Creates a square-shaped area (OverlapBoxAll) and collects all Collider2D objects within that area
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            worldPoint, new Vector2(sizeOfInteractableArea, sizeOfInteractableArea), 0f);

        // View all found objects
        foreach (Collider2D c in colliders)
        {
            // If the object has a ToolHit component, calls the Hit() method.
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
