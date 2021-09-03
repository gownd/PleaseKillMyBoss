using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Boss.Core;
using Boss.Object;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 50f;
    [SerializeField] MMFeedbacks hitFeedback = null;
    float currentHealth;

    bool isAlive = true;

    private void Start() 
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        hitFeedback.PlayFeedbacks();

        currentHealth = Mathf.Max(currentHealth - damage, 0f);
        if(currentHealth <= 0f)
        {
            Die();
        }
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
