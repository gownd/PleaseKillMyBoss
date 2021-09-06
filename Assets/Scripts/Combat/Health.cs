using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Boss.Core;
using Boss.Object;
using Boss.FX;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 50f;
    [SerializeField] MMFeedbacks hitFeedback = null;
    [SerializeField] DamageText damageTextPrefab = null;
    [SerializeField] Transform floatingPos = null;
    
    float currentHealth;

    bool isAlive = true;

    private void Start() 
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {
        hitFeedback.PlayFeedbacks();

        DamageText damageText = Instantiate(damageTextPrefab, floatingPos.position, Quaternion.identity);
        damageText.SetText(damage);

        currentHealth = Mathf.Max(currentHealth - damage, 0f);
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
    }

    void Die()
    {
        isAlive = false;
        GetComponent<Animator>().SetTrigger("die");

        ActionObject actionObject = GetComponent<ActionObject>();
        if(actionObject != null) // if Enemy
        {
            actionObject.Disable();
            actionObject.EndActionWithThisObject();
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
