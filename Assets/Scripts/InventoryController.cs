using UnityEngine;

/// <summary>
/// Kontroliuoja langu rodyma, laiko sustabdyma
/// </summary>

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;//pgr inventoriaus langas
    [SerializeField] GameObject tollBarPanel;//tool bar juosta
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject storePanel;
    public static bool IsInventoryOpen { get; private set; }//ar atodarytas inventorius

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if(panel.activeInHierarchy == false)
            {
                Open();
            }
            else
            {
                Close();
            }

            
            // bool newState = !panel.activeInHierarchy;
            // panel.SetActive(newState);
            // IsInventoryOpen = newState;

            // // Sustabdyti žaidimo laiką
            // if (IsInventoryOpen)
            //     Time.timeScale = 0f;
            // else
            //     Time.timeScale = 1f;

            // tollBarPanel.SetActive(!tollBarPanel.activeInHierarchy);
            // statusPanel.SetActive(!statusPanel.activeInHierarchy);
            // storePanel.SetActive(false);
        }
    }

    public void Open()
    {
        Time.timeScale = 0f;
        bool newState = !panel.activeInHierarchy;
        panel.SetActive(true);
        IsInventoryOpen = newState;

        

        tollBarPanel.SetActive(false);
        statusPanel.SetActive(true);
        storePanel.SetActive(false);
    }

    public void Close()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
        tollBarPanel.SetActive(true);
        statusPanel.SetActive(false);
        //storePanel.SetActive(true);
    }
}
