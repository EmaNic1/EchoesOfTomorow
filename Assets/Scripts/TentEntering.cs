using UnityEngine;
using UnityEngine.SceneManagement;

public class TentEntering : Interactable
{
    [SerializeField] private string sceneToLoad = "Tent";

    public override void Interact(Charater character)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
