using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    private Player playerStats;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Vector2 direction;
    private Vector2 facingDirection;
    private bool flipSprite, canDash, dashing;
    private float moveSpeed, dashDistance, dashDuration, dashCooldown;

    private void Start()
    {
        playerStats = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        moveSpeed = playerStats.moveSpeed;
        dashDistance = playerStats.dashDistance;
        dashDuration = playerStats.dashDuration;
        dashCooldown = playerStats.dashCooldown;
        
        flipSprite = false;
        canDash = true;
        facingDirection = Vector2.right;
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
            facingDirection = direction.normalized;
            spriteRenderer.flipX = flipSprite;
            animator.SetBool("PlayerMoving", true);
        }
        else
        {
            animator.SetBool("PlayerMoving", false);
        }

        if (!dashing)
        {
            rigidBody2D.MovePosition(rigidBody2D.position + direction * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;

        Vector2 dashVector = facingDirection * dashDistance;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            rigidBody2D.MovePosition(rigidBody2D.position + dashVector * Time.deltaTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
