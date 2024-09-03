using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Vector2 _direction;
    private Vector2 _facingDirection;
    private bool _flipSprite;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        _flipSprite = false;
        _facingDirection = Vector2.right;
    }

    private void Update()
    {
        Movement();
        Dash();
    }

    private void FixedUpdate()
    {
        Stamina();
    }

    private void Movement()
    {
        _direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            _direction.x = -1;
            _flipSprite = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction.x = 1;
            _flipSprite = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _direction.y = -1;
        }

        if (_direction.x != 0 || _direction.y != 0)
        {
            _facingDirection = _direction.normalized;
            _playerManager.FlipSpriteX(_flipSprite);
            _playerManager.PlayerAnimationMoving(true);
            _playerManager.PlayerMovePosition(_direction);
        }
        else
        {
            _playerManager.PlayerAnimationMoving(false);
        }
    }

    private void Dash()
    {
        // if (_playerManager.)
        // {

        // }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashTrigger();
        }
    }

    private void DashTrigger()
    {
        _playerManager.PlayerDash(_facingDirection);
    }

    private void Stamina()
    {
        _playerManager.RegenStamina();
    }

    // public void UseStamina()
    // {
        
    // }

    // private void StartStaminaRegen()
    // {
    //     _staminaRegenation = true;
    // }

    // private void StopStaminaRegen()
    // {
    //     _staminaRegenation = false;
        
    // }
}
