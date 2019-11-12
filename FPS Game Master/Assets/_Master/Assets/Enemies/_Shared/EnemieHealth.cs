using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void HealDamage(int damageAmmount)
    {
        currentHealth += damageAmmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void TakeDamage(int damageAmmount)
    {
        currentHealth -= damageAmmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            IDie dieInterface = GetComponent<IDie>();
            if (dieInterface != null)
            {
                dieInterface.Die(damageAmmount);
            }
        }
    }



}
