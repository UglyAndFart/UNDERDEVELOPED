using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;
    private Enemy enemy;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private Vector2 currentPosition;
    private float distance, moveSpeed, aggroRange, attackRange, attackSpeed;
    private bool awoken, attacking;
    
    // Parameters for circular movement
    [SerializeField]
    private float circleRadius = 3f; // Radius of the circle around the player
    [SerializeField]
    private float circleSpeed = 1f; // Speed at which the boss circles around the player
    private float angle;

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
        awoken = false;
        attacking = false;
        angle = 0f;
    }

    private void Update()
    {
        if (attacking)
        {
            return;
        }

        if (awoken)
        {
            MoveInCircle();
            if (distance <= attackRange)
            {
                Attack();        
            }
        }
        else
        {
            CheckAwoken();
        }
    }

    private void CheckAwoken()
    {
        currentPosition = transform.position;
        distance = Vector2.Distance(currentPosition, player.position);

        if (distance <= aggroRange)
        {
            awoken = true;
        }
    }

    private void Move()
    {
        currentPosition = transform.position;
        distance = Vector2.Distance(currentPosition, player.position);

        if (distance <= aggroRange)
        {
            awoken = true;
        }

        if(!awoken)
        {
            return;
        }
        
        Vector2 direction = player.position - currentPosition;
        direction.Normalize();
        spriteRenderer.flipX = direction.x < 0 ?true :false;
        animator.SetBool("Moving", true);
        rigidBody2D.MovePosition(currentPosition + direction * moveSpeed * Time.deltaTime);
    }

    private void MoveInCircle()
    {
        angle += circleSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * circleRadius;
        float y = Mathf.Sin(angle) * circleRadius;
        Vector2 offset = new Vector2(x, y);
        
        Vector2 targetPosition = (Vector2)player.position + offset;
        currentPosition = transform.position;

        Vector2 direction = targetPosition - currentPosition;
        direction.Normalize();
        spriteRenderer.flipX = direction.x < 0 ? true : false;

        animator.SetBool("Moving", true);
        rigidBody2D.MovePosition(Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime));
    }

    private void Attack()
    {
        int attackNumber = UnityEngine.Random.Range(1, 4);
        animator.SetTrigger($"Attack{attackNumber}");
    }

    public void EnemyAttackStart()
    {
        attacking = true;
    }

    public void EnemyAttackEnd()
    {
        attacking = false;
    }
}
