using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField] ScreenTint screenTint;
    string currentScene;

    AsyncOperation unload;
    AsyncOperation load;   

    bool respawTransition; 

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to, targetPosition));
    }

    internal void Respawn(Vector3 respawnPointPosition, string respawnPointScene)
    {
        respawTransition = true;
        if(currentScene != respawnPointScene)
        {
            InitSwitchScene(respawnPointScene, respawnPointPosition);
        }
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {

        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);

        yield return SwitchSceneCoroutine(to, targetPosition);

        screenTint.UnTint();
    }

    public void SwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(SwitchSceneCoroutine(to, targetPosition));
    }

    private IEnumerator SwitchSceneCoroutine(string to, Vector3 targetPosition)
    {
        Debug.Log("▶ Pradedam krauti sceną: " + to);

        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        yield return load;

        Scene newScene = SceneManager.GetSceneByName(to);
        Debug.Log("✅ Nauja scena užkrauta: " + newScene.name);

        SceneManager.SetActiveScene(newScene);
        Debug.Log("✅ Nauja scena nustatyta kaip aktyvi");

        // --- DEBUG prieš player ---
        Debug.Log("GameManager.instance: " + GameManager.instance);
        Debug.Log("GameManager.instance.player: " + GameManager.instance?.player);

        Transform player = GameManager.instance?.player?.transform;
        if (player == null)
        {
            Debug.LogError("❌ Player yra NULL – patikrink GameManager!");
            yield break; // sustabdo coroutine, kad nelūžtų
        }

        player.position = targetPosition;
        Debug.Log("✅ Player perkeltas į: " + targetPosition);

        SceneManager.MoveGameObjectToScene(player.gameObject, newScene);
        Debug.Log("✅ Player perkeltas į naują sceną");

        // --- DEBUG prieš kamerą ---
        Debug.Log("Camera.main: " + Camera.main);
        CameraMovemnet cam = Camera.main?.GetComponent<CameraMovemnet>();
        Debug.Log("CameraMovemnet komponentas: " + cam);

        if (cam != null)
        {
            cam.SnapToTarget(player.position);
            Debug.Log("✅ Kamera perkelta į player poziciją");
        }
        else
        {
            Debug.LogWarning("⚠ Kamera nerasta arba neturi CameraMovemnet komponento!");
        }

        unload = SceneManager.UnloadSceneAsync(currentScene);
        yield return unload;
        Debug.Log("✅ Sena scena iškrauta: " + currentScene);

        currentScene = to;

        if (respawTransition)
        {
            player.GetComponent<Charater>().FullHeal();
            player.GetComponent<Charater>().FullRest(100);
            player.GetComponent<DisableControls>().EnableControl();
            respawTransition = false;
            Debug.Log("✅ Player respawn baigtas");
        }
    }
}
