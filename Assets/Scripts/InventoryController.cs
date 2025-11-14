using UnityEngine;

/// <summary>
/// Kontroliuoja langu rodyma, laiko sustabdyma
/// </summary>

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;//pgr inventoriaus langas
    [SerializeField] GameObject tollBarPanel;//tool bar juosta
    public static bool IsInventoryOpen { get; private set; }//ar atodarytas inventorius

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool newState = !panel.activeInHierarchy;
            panel.SetActive(newState);
            IsInventoryOpen = newState;

            // Sustabdyti žaidimo laiką
            if (IsInventoryOpen)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;

            tollBarPanel.SetActive(!tollBarPanel.activeInHierarchy);
        }
    }
}
