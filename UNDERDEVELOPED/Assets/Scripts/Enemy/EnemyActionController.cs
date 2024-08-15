using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private float speed, aggroRange;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private Vector2 currentPosition;
    private float distance;

    private void Start()
    {
        animator = GetComponent<Animator>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
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
        rigidBody2D.MovePosition(currentPosition + direction * speed * Time.deltaTime);
        
    }
}
