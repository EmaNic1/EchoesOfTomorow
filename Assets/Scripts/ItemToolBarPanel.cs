using UnityEngine;

/// <summary>
/// Irankiu juosta
/// </summary>

public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] private ToolBarController controller;
    private int currentSelected = 0;

    private void Start()
    {
        Init();//nustato mygtuku indeksus
        controller.onChange += HighLight;//parodoma su mark, kad zaidejas yra pasirinkes kazkoki objekta

        currentSelected = -1;
        //automatiskai uzdeda mark ant pirmo rasto objekto
        for (int i = 0; i < buttons.Count; i++)
        {
            if (inventory.slot[i].items != null)
            {
                currentSelected = i;
                break;
            }
        }

        //jeigu rado, atnaujina
        if (currentSelected != -1)
            UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        if (buttons == null || buttons.Count == 0) return;

        //einame per visus mygtukus ir pazymi objekta kuris sutampa su currentSeleceted
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Hihghlight(i == currentSelected);
    }

    /// <summary>
    /// Paspaudus ant objekto jis pazymimas
    /// </summary>
    /// <param name="id"></param>
    public override void OnClick(int id)
    {
        if (controller != null)
            controller.Set(id);

        HighLight(id);
    }

    public void HighLight(int id)
    {
        if (buttons == null || buttons.Count == 0) return;

        //isjungia sena pazymeta objekta
        if (currentSelected >= 0 && currentSelected < buttons.Count)
            buttons[currentSelected].Hihghlight(false);

        currentSelected = Mathf.Clamp(id, 0, buttons.Count - 1);

        //atnaujina, kai yra pazymimas kitas objektas
        if (currentSelected >= 0 && currentSelected < buttons.Count)
            buttons[currentSelected].Hihghlight(true);
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.onChange -= HighLight;
    }

    public override void Show()
    {
        base.Show();
        controller.UpdateHighlitghIcon();
    }
}