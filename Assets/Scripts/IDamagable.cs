using UnityEngine;

public interface IDamagable
{
    public void CalculateDamage(ref int damage);
    public void ApplyDamage(int damage);
    public void CheckState();
}
