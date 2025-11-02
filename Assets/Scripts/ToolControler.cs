using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolControler : MonoBehaviour
{
    CharacterController2D character; // takes the lastMotion of the object
    Rigidbody2D rb; // Object
    ToolBarController toolBarController;
    Animator animator;
    [SerializeField] float offsetDistance = 1f; // How far away from the character the tool will be used
    [SerializeField] float sizeOfInteractableArea = 1.2f; // What will be the area of ​​the "square" where
                                                          // objects that can be hit are checke
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    //[SerializeField] CropsManager cropsManager;
    //[SerializeField] TileData plowableTiles;
    Vector3Int selecetedTilePosition;
    bool selected;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        toolBarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// If the left mouse button is pressed, calls the UseTool() method
    /// </summary>
    private void Update()
    {
        SelectTile();
        CabSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selecetedTilePosition = tileMapReadController.GetGridBase(Input.mousePosition, true);
    }

    void CabSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selected = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selected);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selecetedTilePosition;
    }

    private bool UseToolWorld()
    {
        // Calculates the location of a point in front of the character (based on the last direction of movement)
        Vector2 position = rb.position + character.lastMotionVector * offsetDistance;

        Items item = toolBarController.GetItems;
        if (item == null)
        {
            return false;
        }
        if(item.onAction == null)
        {
            return false;
        }

        animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position);

        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
            }
        }

        return complete;
    }

    private void UseToolGrid()
    {
        if (selected == true)
        {
            Items item = toolBarController.GetItems;
            if (item == null)
            {
                return;
            }
            if(item.onTileMapAction == null)
            {
                return;
            }

            animator.SetTrigger("act");
            bool complete = item.onTileMapAction.OnApplyToTileMap(selecetedTilePosition, tileMapReadController);

            if(complete == true)
            {
                if(item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }
        }
    }
}
