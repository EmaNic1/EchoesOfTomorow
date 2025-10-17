using UnityEngine;

public class ToolControler : MonoBehaviour
{
    CharacterController2D character; // takes the lastMotion of the object
    Rigidbody2D rb; // Object
    [SerializeField] float offsetDistance = 1f; // How far away from the character the tool will be used
    [SerializeField] float sizeOfInteractableArea = 1.2f; // What will be the area of ​​the "square" where
                                                          // objects that can be hit are checked.

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// If the left mouse button is pressed, calls the UseTool() method
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        // Calculates the location of a point in front of the character (based on the last direction of movement)
        Vector2 position = rb.position + character.lastMotionVector * offsetDistance;

        // Creates a square-shaped area (OverlapBoxAll) and collects all Collider2D objects within that area
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            position,
            new Vector2(sizeOfInteractableArea, sizeOfInteractableArea),
            0f
        );

        // View all found objects
        foreach (Collider2D c in colliders)
        {
            // If the object has a ToolHit component, calls the Hit() method.
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
