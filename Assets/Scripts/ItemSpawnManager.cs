using UnityEngine;

/// <summary>
/// singelton klase
/// spawnina objektus i scena(pvz drop item is medzio, akmens...)
/// </summary>

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;

    /// <summary>
    /// Irasomas objektas
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickUpItemPrefab;//objektas kuris yra surenkamas

    /// <summary>
    /// Sukuria nauja objekta, nustato jo tipa, ir kieki
    /// </summary>
    /// <param name="position"></param>
    /// <param name="items"></param>
    /// <param name="count"></param>
    public void SpawnItem(Vector3 position, Items items, int count)
    {
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity);
        o.GetComponent<PickUpItem>().Set(items, count);//pickupitem gauna informacija apie nauja objekta
    }
}
