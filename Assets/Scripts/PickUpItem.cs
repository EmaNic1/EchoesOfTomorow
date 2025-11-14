using UnityEngine;

/// <summary>
/// Objektas, kuris yra ant zemes
/// </summary>

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;

    public Items items;
    public int count = 1;


    private void Awake()
    {
        //Kai pasirodo objektas, jis suranda žaidėją iš „GameManager“.
        player = GameManager.instance.player.transform;
    }

    public void Set(Items items, int count)
    {
        this.items = items;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = items.icon;
    }

    private void Update()
    {
        // kai ttl pasiekia 0, objektas sunaikinamas
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }

        // Skaiciuojamas atstumas tarp objekto ir zaidejo
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > pickUpDistance)
        {
            // nieko nevyksta kai zaidejas toli
            return;
        }

        // jeigu zaidejas yra pakankamai arti, objektas pradeda judeti link jo
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // kai objektas pasiekia zaideja, jis yra idedamas i inventoriu
        if (distance < 0.1f)
        {
            if(GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(items, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }
            Destroy(gameObject);
        }
    }
}
