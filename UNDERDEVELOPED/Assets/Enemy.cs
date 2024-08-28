using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health, physicalDamage, magicDamage;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void TakeDamage(float damage)
    {
        animator.SetTrigger("TakeDamage");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("EnemyAlive", false);
    }
}
