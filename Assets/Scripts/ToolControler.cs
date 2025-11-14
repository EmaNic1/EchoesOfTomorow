using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolControler : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rb; // Object
    ToolBarController toolBarController;
    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
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
        SelectTile();//apskaičiuoja, kuri plytelė po pelės žymekliu
        CabSelectCheck();//tikrina, ar žaidėjas pakankamai arti plytelės
        Marker();//vizualizacija
        if (Input.GetMouseButtonDown(0))
        {
            //bandomas pasaulio veiksmas
            if (UseToolWorld() == true)
            {
                return;
            }
            //Jei pasaulyje nebuvo veiksmų, bandomas Tilemap veiksmas
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
        // Skaičiuoja vietą priekyje žaidėjo, kur bus naudojamas įrankis.
        Vector2 position = rb.position + character.lastMotionVector * offsetDistance;

        //Gauname įrankį, kurį žaidėjas pasirinko įrankių juostoje
        Items item = toolBarController.GetItems;
        if (item == null)
        {
            return false;//jei nėra jokio įrankio, metodas baigiasi
        }
        if(item.onAction == null)
        {
            return false;//įrankis neturi world veiksmo logikos
        }

        //Paleidžia įrankio animaciją.
        animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position); //Panaudojame įrankį pasaulyje

        //jei objektas yra sunaudojamas
        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);//sumažina kiekį inventoriuje.
            }
        }

        return complete;
    }

    private void UseToolGrid()
    {
        //tikriname, ar žaidėjas yra pakankamai arti plytelės
        if (selected == true)
        {
            Items item = toolBarController.GetItems;
            if (item == null)
            {
                return; //nėra įrankio
            }
            if(item.onTileMapAction == null)
            {
                return; //įrankis neturi Tilemap veiksmo logikos
            }

            //Paleidžia įrankio animaciją.
            animator.SetTrigger("act");
            //Panaudojame įrankį ant plytelės
            bool complete = item.onTileMapAction.OnApplyToTileMap(selecetedTilePosition, tileMapReadController);

            //Jei veiksmas sėkmingas, sunaudojame įrankį inventoriuje.
            if (complete == true)
            {
                if(item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }
        }
    }
}
