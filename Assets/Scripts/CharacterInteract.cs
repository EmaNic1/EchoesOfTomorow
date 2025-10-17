using System;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rb; // Object
    [SerializeField] float offsetDistance = 1f; // How far away from the character the tool will be used
    [SerializeField] float sizeOfInteractableArea = 1.2f; // What will be the area of ​​the "square" where
                                                          // objects that can be hit are checked.
    Charater cha;
    [SerializeField] MarkController markCotroller;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        cha = GetComponent<Charater>();
    }

    /// <summary>
    /// If the left mouse button is pressed, calls the Interact() method
    /// </summary>
    private void Update()
    {
        Check();

        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    private void Check()
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
            // If the object has a Interactable component, calls the Interact() method.
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                markCotroller.Mark(hit.gameObject);
                return;
            }
        }

        markCotroller.Hide();
    }

    private void Interact()
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
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(cha);
                break;
            }
        }
    }
}
