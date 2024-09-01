using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health, physicalDamage, magicDamage, aggroRange, moveSpeed, attackRange, attackSpeed;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void TakeDamage(float damage)
    {
        animator.SetTrigger("Hurt");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("Alive", false);
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetAggroRange()
    {
        return aggroRange;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
