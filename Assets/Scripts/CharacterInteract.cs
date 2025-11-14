using System;
using UnityEngine;

/// <summary>
/// valdo tai ka daro veikejas
/// </summary>

public class CharacterInteract : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rb; // Object
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    Charater cha;
    [SerializeField] MarkController markCotroller;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        cha = GetComponent<Charater>();
    }

    /// <summary>
    /// Jei paspaudžiamas kairysis pelės mygtukas, iškviečiamas Interact() metodas.
    /// </summary>
    private void Update()
    {
        Check();

        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    /// <summary>
    /// vizualiai parodyti, ar veikėjas gali su kuo nors sąveikauti.
    /// </summary>
    private void Check()
    {
        //Apskaičiuoja taško, esančio priešais veikėją, vietą (remiantis paskutine judėjimo kryptimi)
        Vector2 position = rb.position + character.lastMotionVector * offsetDistance;

        // Sukuria kvadrato formos sritį („OverlapBoxAll“) ir surenka visus „Collider2D“ objektus toje srityje.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            position,
            new Vector2(sizeOfInteractableArea, sizeOfInteractableArea),
            0f
        );

        // Peržiūrėti visus rastus objektus
        foreach (Collider2D c in colliders)
        {
            // Jei objektas turi Interactable komponentą, iškviečiamas Mark() metodas.
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                markCotroller.Mark(hit.gameObject);
                return;
            }
        }

        markCotroller.Hide();
    }

    /// <summary>
    /// metodas kviečiamas, paspaudus dešinį pelės mygtuką
    /// </summary>
    private void Interact()
    {
        // Apskaičiuoja taško, esančio priešais veikėją, vietą (remiantis paskutine judėjimo kryptimi)
        Vector2 position = rb.position + character.lastMotionVector * offsetDistance;

        // Sukuria kvadrato formos sritį („OverlapBoxAll“) ir surenka visus „Collider2D“ objektus toje srityje.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            position,
            new Vector2(sizeOfInteractableArea, sizeOfInteractableArea),
            0f
        );

        // Peržiūrėti visus rastus objektus
        foreach (Collider2D c in colliders)
        {
            // Jei objektas turi Interactable komponentą, iškviečiamas Interact() metodas.
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(cha);
                break;
            }
        }
    }
}
