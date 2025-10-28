using UnityEngine;

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
        // When an item appears, it finds a player from the GameManager.
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
        // If the ttl reaches 0, the item disappears (so as not to accumulate in the world).
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }

        // Calculates the distance between an object and the player.
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > pickUpDistance)
        {
            // Nothing happens if the player is too far from the object
            return;
        }

        // If the player is close enough - the object moves towards him.
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // When an item reaches the player (very close), it is destroyed
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
