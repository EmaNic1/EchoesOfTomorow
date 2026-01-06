using UnityEngine;

public class BreakableObjeckt : MonoBehaviour, IDamagable
{
    [SerializeField] int hp = 10;
    [SerializeField] AudioClip onOpenAudio;

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        AudioManager.instance.Play(onOpenAudio);
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
