using UnityEngine;

/// <summary>
/// veliau sutvarkyti kad veiktu atsiradimas
/// </summary>

public class BookSpawner : MonoBehaviour
{
    public static BookSpawner Instance;

    private void Start()
    {
        if (PlayerProfession.Instance != null &&
            PlayerProfession.Instance.currentProfession != null)
        {
            SpawnProfessionBook(PlayerProfession.Instance.currentProfession.professionBookPrefab);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnProfessionBook(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Profesijos knyga nenustatyta!");
            return;
        }

        // Spawn į šios scenos erdvę
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
