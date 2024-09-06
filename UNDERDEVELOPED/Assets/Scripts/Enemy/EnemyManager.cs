using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private bool _spriteFacingRight = true;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private Vector2 _attackPointOffset;

    private Enemy _enemy;
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private float _moveSpeed, _aggroRange, _attackRange;
    private Vector2 _totalAttackOffset;
    private bool _alive = true;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveSpeed = _enemy.GetMoveSpeed();
        _aggroRange = _enemy.GetAggroRange();
        _attackRange = _enemy.GetAttackRange();
    }

    void Update()
    {

    }

    public void EnemyAnimationMoving(bool moving)
    {
        _animator.SetBool("Moving", moving);
    }

    public void EnemyMovePosition(Vector2 direction, Vector2 currentPosition)
    {
        _rigidBody2D.MovePosition(currentPosition + direction * _moveSpeed * Time.deltaTime);
    }

    public void FlipSpriteX(Vector2 direction)
    {
        if (_spriteFacingRight)
        {
            _spriteRenderer.flipX = direction.x < 0 ?true :false;
            _totalAttackOffset = _spriteRenderer.flipX? new Vector2(_attackPointOffset.x * -1f, _attackPointOffset.y): new Vector2(_attackPointOffset.x, _attackPointOffset.y);
        }
        else
        {
            _spriteRenderer.flipX = direction.x > 0 ?true :false;
            _totalAttackOffset = _spriteRenderer.flipX? new Vector2(_attackPointOffset.x, _attackPointOffset.y): new Vector2(_attackPointOffset.x * -1f, _attackPointOffset.y);
        }
    }

    public void AttackAnimation(string name)
    {
        _attackPoint.position = (Vector2)transform.position + _totalAttackOffset;
        _animator.SetTrigger(name);
    }

    public void Attack()
    {
        _playerManager.TakeDamage(_enemy.GetPhysicalDamage());
    }

    public void TakeDamage(float damage)
    {
        if ((_enemy.GetHealth() - damage) <= 0)
        {   
            _alive = false;
            _animator.SetBool("Alive", _alive);
        }

        _animator.SetTrigger("Hurt");
        _enemy.DeductHealth(damage);
    }

    public void DealDamage(Rigidbody2D player)
    {
        player.GetComponent<PlayerManager>().TakeDamage(_enemy.GetPhysicalDamage());
    }

    public void OnDeathStart()
    {
        _enemy.GetComponents<Collider2D>()[0].enabled = false;
        _enemy.GetComponents<Collider2D>()[1].enabled = false;
    }

    public void Die()
    {
        _enemy.gameObject.SetActive(false);
    }

    public float GetAggroRange()
    {
        //Debug.Log($"{transform.name} aggroRange: {_aggroRange}");
        return _aggroRange;
    }

    public float GetAttackRange()
    {
        return _attackRange;
    }

    public bool GetLifeState()
    {
        return _alive;
    }
}
