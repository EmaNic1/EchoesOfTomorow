using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 2f;
    [SerializeField] float visionRange = 5f;   // kiek toli mato žaidėją
    [SerializeField] Vector2 attackSize = Vector2.one;
    [SerializeField] int damage = 1;
    [SerializeField] float timeToAttack = 2f;
    float attackTimer;

    bool playerInSight;

    void Start()
    {
        player = GameManager.instance.player.transform;
        attackTimer = Random.Range(0, timeToAttack);

        Collider2D enemyCol = GetComponent<Collider2D>(); 
        Collider2D playerCol = player.GetComponent<Collider2D>(); 
        Physics2D.IgnoreCollision(enemyCol, playerCol, true);
    }

    void Update()
    {
        // tikrinam ar player yra matymo zonoje
        float distance = Vector2.Distance(transform.position, player.position);
        playerInSight = distance <= visionRange;

        if (playerInSight)
        {
            // judėjimas link player
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            Attack();
        }
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer > 0f) return;

        attackTimer = timeToAttack;

        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);
        for (int i = 0; i < targets.Length; i++)
        {
            // IGNORUOJAM SAVE
            if (targets[i].gameObject == gameObject) continue;

            Damagable charater = targets[i].GetComponent<Damagable>();
            if (charater != null)
            {
                charater.TakeDamage(damage);
            }
        }
    }


    // kad matytum zonoje editoriuje
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, attackSize);
    }
}
