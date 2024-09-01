using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _moveSpeed, _dashDistance, _dashDuration, _dashCooldown;
    
    private void Start()
    {
        _player = GetComponent<Player>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveSpeed = _player.GetMoveSpeed();
        _dashDistance = _player.GetDashDistance();
        _dashDuration = _player.GetDashDuration();
        _dashCooldown = _player.GetDashCooldown();
    }

    public void PlayerMovePosition(Vector2 direction)
    {
        _rigidBody2D.MovePosition(_rigidBody2D.position + direction * _moveSpeed * Time.deltaTime);
    }

    public void PlayerDash(Vector2 dashVector)
    {
       _rigidBody2D.MovePosition(_rigidBody2D.position + dashVector * Time.deltaTime / _dashDuration);
    }

    public void FlipSpriteX(bool flipSprite)
    {
        _spriteRenderer.flipX = flipSprite;
    }

    public void PlayerAnimationMoving(bool moving)
    {
        _animator.SetBool("Moving", moving);
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("Hurt");
        _player.DeductHealth(damage);
    }

    public void PlayerAnimationDeath()
    {
        _animator.SetBool("Alive", false);
    } 

    public float GetDashDistance()
    {
        return _dashDistance;
    }

    public float GetDashDuration()
    {
        return _dashDuration;
    }

    public float GetDashCooldown()
    {
        return _dashCooldown;
    }
}
