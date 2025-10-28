using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject tollBarPanel;
    public static bool IsInventoryOpen { get; private set; }

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
