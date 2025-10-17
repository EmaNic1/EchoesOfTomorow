using UnityEngine;

/// <summary>
/// This is a simple form of singleton-style usage that 
/// allows you to have one centralized controller.
/// </summary>


public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
}
