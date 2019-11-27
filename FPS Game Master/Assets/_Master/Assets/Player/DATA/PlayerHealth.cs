using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public static event Action OnPlayerDeath = delegate { };

    [SerializeField] private int maxHealth;
    private int currentHelath;
    private void OnEnable()
    {
        HealthPack.OnHeal += HealDamage;
    }

    private void OnDisable()
    {
        HealthPack.OnHeal -= HealDamage;
    }

    private void Start()
    {
        currentHelath = maxHealth;
        PlayerInfo.PlayerMaxHealth = maxHealth;
        PlayerInfo.PlayerCurrentHealth = currentHelath;
    }
    public void HealDamage(int healthAmmount)
    {
        currentHelath += healthAmmount;
        PlayerInfo.PlayerCurrentHealth = currentHelath;
        if (currentHelath>maxHealth)
        {
            currentHelath = maxHealth;
            PlayerInfo.PlayerCurrentHealth = currentHelath;
        }

    }

    public void TakeDamage(int damageAmmount)
    {
        currentHelath -= damageAmmount;
        PlayerInfo.PlayerCurrentHealth = currentHelath;
        if (currentHelath <= 0)
        {
            PlayerInfo.PlayerCurrentHealth = currentHelath;
            OnPlayerDeath();
        }
    }        
}
