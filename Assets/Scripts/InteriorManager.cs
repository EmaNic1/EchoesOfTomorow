using UnityEngine;

public class InteriorManager : MonoBehaviour
{
    public Transform spawnPoint; // InteriorSpawn

    private void Start()
    {
        // Perkeliam player į interjerą
        var player = GameManager.instance.player;
        player.transform.position = spawnPoint.position;
        player.SetActive(true);

        // Perjungiam kamerą, jei reikia (jeigu turi vidinę)
        // CameraManager.Instance.SwitchToInterior();
    }
}
