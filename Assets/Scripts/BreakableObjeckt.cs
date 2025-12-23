using UnityEngine;

public class BreakableObjeckt : MonoBehaviour, IDamagable
{
    [SerializeField] int hp = 10;

    public void ApplyDamage(int damage)
    {
        hp -= damage;
    }

    public void CalculateDamage(ref int damage)
    {
        damage = 1;
    }

    public void CheckState()
    {
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
