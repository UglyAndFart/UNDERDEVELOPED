using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    private PlayerManager _playerManager;
    private GameObject _attackPoint;
    private Vector2 _direction;
    private Vector2 _facingDirection;
    private bool _flipSprite;
    private List<Collider2D> _enemyHits;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        _attackPoint = transform.Find("Attack Pivot").gameObject;
        _flipSprite = false;
        _facingDirection = Vector2.right;
        _enemyHits = new List<Collider2D>();
    }

    private void Update()
    {
        Movement();
        Dash();
        Attack();
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

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _attackPoint.SetActive(true);

        }

        if (_enemyHits != null)
        {
            _playerManager.DealDamage(_enemyHits);
            _enemyHits = new List<Collider2D>();
        }
    }

    public void AddEnemyToArray(Collider2D enemyHit)
    {
        _enemyHits.Add(enemyHit);
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
