using UnityEngine;

[RequireComponent(typeof(TimeAgent))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Items toSpawn;
    [SerializeField] int count;
    [SerializeField] float spread = 2f;
    [SerializeField] float probability = 0.15f;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += Spawn;
    }

    void Spawn()
    {
        if (UnityEngine.Random.value < probability)
        {
            // pradine padetis yra ta kurioje stovi naikinamas objektas
            Vector3 position = transform.position;

            // position.x ir position.y keičiami atsitiktinai, kad elementai nebūtų vienoje vietoje, o būtų išmėtyti po objektą.
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(position, toSpawn, count);
            // pozicija nustatoma pagal apskaičiuotą poziciją
            //GameObject go = Instantiate(pickUpDrop);
            //go.transform.position = position;
        }

    }
}
