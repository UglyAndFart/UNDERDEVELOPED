using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;
    private Enemy enemy;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private Vector2 currentPosition;
    private float distance, moveSpeed, aggroRange, attackRange, attackSpeed;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        moveSpeed = enemy.GetMoveSpeed();
        aggroRange = enemy.GetAggroRange();
        attackRange = enemy.GetAttackRange();
        attackSpeed = enemy.GetAttackSpeed();
    }

    private void Update()
    {
        currentPosition = transform.position;
        distance = Vector2.Distance(currentPosition, player.position);

        if (distance > aggroRange)
        {
            animator.SetBool("EnemyMoving", false);
            return;
        }
        
        Vector2 direction = player.position - currentPosition;
        direction.Normalize();
        spriteRenderer.flipX = direction.x < 0 ?true :false;
        animator.SetBool("EnemyMoving", true);
        rigidBody2D.MovePosition(currentPosition + direction * moveSpeed * Time.deltaTime);

        if (distance <= attackRange)
        {
            //animator.SetTrigger("Attack");
        }
    }
}
