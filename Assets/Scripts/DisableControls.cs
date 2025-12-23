using UnityEngine;

public class DisableControls : MonoBehaviour
{
    CharacterController2D characterController2D;
    ToolControler toolControler;
    InventoryController inventoryController;
    ToolBarController toolBarController;

    void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
        toolControler = GetComponent<ToolControler>();
        inventoryController = GetComponent<InventoryController>();
        toolBarController = GetComponent<ToolBarController>();
    }

    public void DisableControl()
    {
        characterController2D.enabled = false;
        toolBarController.enabled = false;
        inventoryController.enabled = false;
        toolBarController.enabled = false;
    }

    public void EnableControl()
    {
        characterController2D.enabled = true;
        toolBarController.enabled = true;
        inventoryController.enabled = true;
        toolBarController.enabled = true;
    }
}
