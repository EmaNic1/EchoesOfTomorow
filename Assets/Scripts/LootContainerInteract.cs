using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool open;

    [SerializeField] ItemContainer itemContainer;


    public override void Interact(Charater charater)
    {
        if(open == false)
        {
            Open(charater);
        }
        else
        {   
            Close(charater);
        }
    }

    public void Open(Charater charater)
    {
        Time.timeScale = 0f;
        open = true;
        closedChest.SetActive(false);
        openedChest.SetActive(true);
        charater.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }

    public void Close(Charater charater)
    {
        Time.timeScale = 1f;
        open = false;
        closedChest.SetActive(true);
        openedChest.SetActive(false);
        charater.GetComponent<ItemContainerInteractController>().Close();
    }
}
