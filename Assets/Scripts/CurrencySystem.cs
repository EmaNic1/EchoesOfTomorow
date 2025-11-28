using TMPro;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText; // tekstas, kuris rodo pinigus
    private int scrap; // tavo valiuta, pvz. Scrap

    private void Start()
    {
        scrap = 500; // pradinė suma, gali keisti
        UpdateUI();
    }

    // Prideda pinigus
    public void AddScrap(int amount)
    {
        scrap += amount;
        UpdateUI();
    }

    // Atima pinigus, grąžina true jei užteko, false jei ne
    public bool SpendScrap(int amount)
    {
        if (scrap >= amount)
        {
            scrap -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough scrap!");
            return false;
        }
    }

    // Tikrina kiek turi pinigų
    public int GetScrap()
    {
        return scrap;
    }

    // Atspindi UI
    private void UpdateUI()
    {
        if (currencyText != null)
            currencyText.text = scrap.ToString();
    }
}
