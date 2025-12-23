using System;
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
    public PlacableObjectRef placableObjects;
    public SelectProfesion selectProfession;
    //public CurrencySystem currencySystem;
    public OnScreeMessageSystems messageSystem;
    public ScreenTint screenTint;
}
