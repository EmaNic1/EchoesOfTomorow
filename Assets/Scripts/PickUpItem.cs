using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;

    private void Awake()
    {
        // When an item appears, it finds a player from the GameManager.
        player = GameManager.instance.player.transform;
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
            Destroy(gameObject);
        }
    }
}
