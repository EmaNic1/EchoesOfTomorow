using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// This is a simple form of singleton-style usage that 
/// allows you to have one centralized controller.
/// </summary>


public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;//veikejas
    public ItemContainer inventoryContainer;//inventorius
    public ItemDragAndDropController dragAndDropController;//drag n drop sistema
    public DayTimeController timeController;
    public DialogSystem dialogSystem;
    public SelectProfesion selectProfession;
    public CurrencySystem currencySystem;

    // public CropsManager cropsManager;
    // public TreeManager treeManager;

    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // Tikrinam ar čia Main scena
    //     if (scene.name == "Main")
    //     {
    //         // Surasti BaseTilemap
    //         GameObject baseTM = GameObject.FindWithTag("BaseTilemap");
    //         if (baseTM != null && treeManager != null)
    //         {
    //             treeManager.SetTargetTilemap(baseTM.GetComponent<Tilemap>());
    //         }

    //         // Surasti CropsTileMap
    //         GameObject cropsTM = GameObject.FindWithTag("CropsTilemap");
    //         if (cropsTM != null && cropsManager != null)
    //         {
    //             cropsManager.SetTargetTilemap(cropsTM.GetComponent<Tilemap>());
    //         }
    //     }
    // }
}
