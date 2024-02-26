using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public Component[] enabledAfterDeath;


    void Start()
    {
        currentHealth = maxHealth;

        StartCoroutine(DeathListener());
    }

    IEnumerator DeathListener()
    {
        while (currentHealth > 0)
        {
            yield return null;
        }
        Die();
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Die()
    {
        foreach (Component component in GetComponents<Component>())
        {
            MonoBehaviour monoBehaviour = component as MonoBehaviour;
            if (monoBehaviour != null && !enabledAfterDeath.Contains(monoBehaviour))
            {
                monoBehaviour.enabled = false;
            }
        }
    }
}