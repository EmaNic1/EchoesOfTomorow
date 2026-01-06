using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string nameEssential;
    [SerializeField] string nameNewGameStart;

    [SerializeField] PlayerData playerData;
    public Gender selectedGender;
    public TMPro.TMP_Text genderText;
    public TMPro.TMP_InputField nameInputField;

    void Start()
    {
        SetGenderFemale();
        UpdateName();
    }

    public void StartGame()
    {
        playerData.ResetData();
        SceneManager.LoadScene(nameNewGameStart, LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssential, LoadSceneMode.Additive);
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void SetGenderMale()
    {
        selectedGender = Gender.Male;
        playerData.characterGender = selectedGender;
        genderText.text = "Male";
    }

    public void SetGenderFemale()
    {
        selectedGender = Gender.Female;
        playerData.characterGender = selectedGender;
        genderText.text = "Female";
    }

    public void UpdateName()
    {
        playerData.characterName = nameInputField.text;
    }

    public void SaveSlot(int num)
    {
        playerData.saveSlotId = num;
    }
}
