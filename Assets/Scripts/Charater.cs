using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Stat
{
    public int maxVal;
    public int currVal;

    public Stat(int curr, int max)
    {
        maxVal = max;
        currVal = curr;
    }

    internal void Substract(int amount)
    {
        currVal -= amount;
    }

    internal void Add(int amount)
    {
        currVal += amount;
        if(currVal > maxVal){ currVal = maxVal; }
    }

    internal void SetToMax()
    {
        currVal = maxVal;
    }
}

public class Charater : MonoBehaviour, IDamagable
{
    public Stat hp;
    [SerializeField] StatusBar hpBar;
    public Stat stamina;
    [SerializeField] StatusBar staminaBar;

    public bool isDead;
    public bool isExhausted;

    DisableControls disableControls;
    PlayerRespawn playerRespawn;

    void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currVal, stamina.maxVal);
    }

    private void UpdateHPBar()
    {
        hpBar.Set(hp.currVal, hp.maxVal);
    }

    public void TakeDamage(int amount)
    {
        if(isDead == true){ return; }
        hp.Substract(amount);
        if(hp.currVal <= 0)
        {
            Dead();
        }

        UpdateHPBar();
    }

    private void Dead()
    {
        isDead = true;
        disableControls.DisableControl();
        playerRespawn.StartResoawn();
    }

    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHPBar();
    }

    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHPBar();
    }

    public void GetTired(int amount)
    {
        stamina.Substract(amount);
        if(stamina.currVal <= 0)
        {
            Exhausted();
        }
        UpdateStaminaBar();
    }

    private void Exhausted()
    {
        isExhausted = true;
        disableControls.DisableControl();
        playerRespawn.StartResoawn();
    }

    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    public void FullRest(int amount)
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }

    public void CalculateDamage(ref int damage)
    {
    }

    public void ApplyDamage(int damage)
    {
        TakeDamage(damage);
    }

    public void CheckState()
    {
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         TakeDamage(20);
    //     }
    //     if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         Heal(20);
    //     }
    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         GetTired(20);
    //     }
    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         Rest(20);
    //     }
    // }
}
