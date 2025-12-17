using System;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    internal void TakeDamage(int damage)
    {
        Destroy(gameObject);
        GameManager.instance.messageSystem.PostMessage(transform.position, damage.ToString());
    }
}
