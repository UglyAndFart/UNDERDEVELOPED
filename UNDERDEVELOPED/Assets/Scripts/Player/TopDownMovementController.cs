using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Vector2 _direction;
    private Vector2 facingDirection;
    private bool flipSprite, canDash = true, dashing = false;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        flipSprite = false;
        facingDirection = Vector2.right;
    }

    private void Update()
    {
        CheckMovement();
        CheckDash();
    }

    private void CheckMovement()
    {
        _direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            _direction.x = -1;
            flipSprite = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction.x = 1;
            flipSprite = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _direction.y = -1;
        }

        if ((_direction.x != 0 || _direction.y != 0) && !dashing)
        {
            facingDirection = _direction.normalized;
            _playerManager.FlipSpriteX(flipSprite);
            _playerManager.PlayerAnimationMoving(true);
            _playerManager.PlayerMovePosition(_direction);
        }
        else
        {
            _playerManager.PlayerAnimationMoving(false);
        }
    }

    private void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;

        Vector2 dashVector = facingDirection * _playerManager.GetDashDistance();
        float elapsedTime = 0f;

        while (elapsedTime < _playerManager.GetDashDuration())
        {
            _playerManager.PlayerDash(dashVector);   
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dashing = false;
        yield return new WaitForSeconds(_playerManager.GetDashCooldown());
        canDash = true;
    }
}
