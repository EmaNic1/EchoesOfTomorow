using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    void Awake()
    {
        instance = this;
    }

    [SerializeField] ScreenTint screenTint;
    string currentScene;

    AsyncOperation unload;
    AsyncOperation load;    

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to, targetPosition));
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();

        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);

        SwitchScene(to, targetPosition);


        while(load != null & unload != null)
        {
            if (load.isDone)
            {
                load = null;
            }
            if (unload.isDone)
            {
                unload = null;
            }
            yield return new WaitForSeconds(0.1f);
        }


        //yield return new WaitForEndOfFrame();

        screenTint.UnTint();
    }

    public void SwitchScene(string to, Vector3 targetPosition)
    {
        Transform playerPosition = GameManager.instance.player.transform;
        playerPosition.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            playerPosition.position.z
        );

        CameraMovemnet cam = Camera.main.GetComponent<CameraMovemnet>();
        cam.SnapToTarget(playerPosition.position);

        StartCoroutine(SwitchSceneCoroutine(to));
    }

    private IEnumerator SwitchSceneCoroutine(string to)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        yield return load;

        // 1. Užkraunam naują sceną additive
        // var loadOp = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        // yield return loadOp;

        // 2. ČIA ĮRAŠOME SetActiveScene, kai scena jau pilnai įkelta
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(to));

        unload = SceneManager.UnloadSceneAsync(currentScene);
        yield return unload;

        // 3. Iškraunam seną sceną
        // var unloadOp = SceneManager.UnloadSceneAsync(currentScene);
        // yield return unloadOp;

        // 4. Atnaujinam currentScene
        currentScene = to;
    }
}
