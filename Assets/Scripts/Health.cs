using JetBrains.Annotations;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth;

    [SerializeField]
    public float StunBarMax;

    [SerializeField]
    private UnityEvent OnDie, OnWin, OnStun, OnRecover;

    public UnityEvent<float, float, float, float> OnDamageDecrease;

    public static EventHandler<ulong> OnPLayerWon;

    public float CurrentHealth;
    public float CurrentStunBar;

    private void Start()
    {
    }

    public void SetupLevel(float maxHealth, float stunMax)
    {
        MaxHealth = maxHealth;
        StunBarMax = stunMax;
        CurrentHealth = MaxHealth;
        CurrentStunBar = 0;
    }

    public void SetHealth(float damage)
    {
        CurrentHealth -= damage;
        CurrentStunBar += damage;
        
        OnDamageDecrease?.Invoke(CurrentHealth, MaxHealth, CurrentStunBar, StunBarMax);
        if (CurrentHealth <= 0)
        {
            OnDie?.Invoke();
        }

        if (CurrentStunBar >= StunBarMax)
        {
            OnStun?.Invoke();
            Invoke("Recover", 5f);
        }
    }

    private void Recover()
    {
        OnRecover?.Invoke();
    }

    public void Heal(float health)
    {
        CurrentHealth += health;
    }

   
}
