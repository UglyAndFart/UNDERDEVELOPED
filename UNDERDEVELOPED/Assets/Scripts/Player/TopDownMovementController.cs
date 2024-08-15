using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    [SerializeField]
    private int speed;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Vector2 direction;
    private bool flipSprite;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flipSprite = false;
    }

    private void Update()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            flipSprite = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            flipSprite = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        if (direction.x != 0 || direction.y != 0)
        {
            spriteRenderer.flipX = flipSprite;
            direction.Normalize();
            animator.SetBool("PlayerMoving", true);
        }
        else
        {
            animator.SetBool("PlayerMoving", false);
        }

        rigidBody2D.MovePosition(rigidBody2D.position + direction * speed * Time.deltaTime);
    }
}
