using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI elementas, kuris atitinka vieta objektui
/// </summary>

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;//objekto icona
    [SerializeField] TextMeshProUGUI text;//objekto kiekis
    [SerializeField] Image highlight;//remelis

    int myIndex;//mygtuko vieta inventoriuje

    ItemPanel itemPanel;

    /// <summary>
    /// Atitinkama vieta su indeksu
    /// </summary>
    /// <param name="index">indeksas</param>
    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void SetItemPanel(ItemPanel source)
    {
        itemPanel = source;
    }

    /// <summary>
    /// Atnaujina mygtuko isvaizda pagal duotus daikto duomenis
    /// </summary>
    /// <param name="slot"></param>
    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.items.icon;
        //Jeigu stacable rodomas tekstas prie iconos, kitu atveju - tekstas nerodomas
        if (slot.items.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Pasalina icona ir jos teksta, jeigu mygtukas atsilaisvina
    /// </summary>
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        text.gameObject.SetActive(false);
    }

    /// <summary>
    /// Koks daiktas buvo pasirinktas
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);
    }

    /// <summary>
    /// Hihghlight efektas
    /// </summary>
    /// <param name="b"></param>
    public void Hihghlight(bool b)
    {
        highlight.gameObject.SetActive(b);
    }
}

