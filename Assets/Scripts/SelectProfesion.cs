using UnityEngine;

public class SelectProfesion : MonoBehaviour
{
    public ProfessionData gardener;
    public ProfessionData forester;
    public ProfessionData mage;

    public GameObject uiPanel; // panelė su mygtukais

    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }

    private void Start()
    {
        Show(false);
    }


    public void ChooseGardener()
    {
        PlayerProfession.Instance.SetProfession(gardener);
        uiPanel.SetActive(false);
    }

    public void ChooseForester()
    {
        PlayerProfession.Instance.SetProfession(forester);
        uiPanel.SetActive(false);
    }

    public void ChooseMage()
    {
        PlayerProfession.Instance.SetProfession(mage);
        uiPanel.SetActive(false);
    }
}
