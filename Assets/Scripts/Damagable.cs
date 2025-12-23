using System;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    IDamagable damagable;

    internal void TakeDamage(int damage)
    {
        if(damagable == null)
        {
            damagable = GetComponent<IDamagable>();
        }
        
        damagable.CalculateDamage(ref damage);
        damagable.ApplyDamage(damage);
        GameManager.instance.messageSystem.PostMessage(transform.position, damage.ToString());
        damagable.CheckState();
    }
}
