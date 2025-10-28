using UnityEngine;

public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] private ToolBarController controller;
    private int currentSelected = 0;

    // Start() su bazinės klasės Start() kvietimu
    private void Start()
    {
        Init();
        controller.onChange += HighLight;

        // Nustatyti currentSelected tik pirmam slot'ui su item
        currentSelected = -1;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (inventory.slot[i].items != null)
            {
                currentSelected = i;
                break;
            }
        }

        // Tik jei randame slot su item, tada highlight
        if (currentSelected != -1)
            UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        if (buttons == null || buttons.Count == 0) return;

        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Hihghlight(i == currentSelected);
    }

    // Kai spaudžiamas toolbar mygtukas
    public override void OnClick(int id)
    {
        if (controller != null)
            controller.Set(id);

        HighLight(id);
    }

    // Highlight logika
    public void HighLight(int id)
    {
        if (buttons == null || buttons.Count == 0) return;

        // Išjungia seną highlight, jei indeksas galiojantis
        if (currentSelected >= 0 && currentSelected < buttons.Count)
            buttons[currentSelected].Hihghlight(false);

        // Nustato naują indeksą saugiai
        currentSelected = Mathf.Clamp(id, 0, buttons.Count - 1);

        // Įjungia naują highlight
        if (currentSelected >= 0 && currentSelected < buttons.Count)
            buttons[currentSelected].Hihghlight(true);
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.onChange -= HighLight;
    }
}