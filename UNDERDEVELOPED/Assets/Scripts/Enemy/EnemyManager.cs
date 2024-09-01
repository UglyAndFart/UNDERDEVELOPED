using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private bool _spriteFacingRight = true;
    [SerializeField]
    private Transform _attackPoint;

    private Enemy _enemy;
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private float _moveSpeed, _aggroRange, _attackRange;
    private LayerMask _playerLayer;
    private Vector2 _attackPointOffset;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveSpeed = _enemy.GetMoveSpeed();
        _aggroRange = _enemy.GetAggroRange();
        _attackRange = _enemy.GetAttackRange();
        _playerLayer = _enemy.GetPlayerLayer();
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
            _attackPointOffset = (direction.x > 0)? new Vector3(.5f, 0.2f): new Vector2(-.5f, 0.2f);
        }
        else
        {
            _spriteRenderer.flipX = direction.x > 0 ?true :false;
            _attackPointOffset = (direction.x < 0)? new Vector3(-.5f, 0.2f): new Vector2(.5f, 0.2f);
        }
    }

    public void AttackAnimation(string name)
    {
        _animator.SetTrigger(name);
    }

    public void Attack()
    {
        _attackPoint.position = (Vector2)transform.position + _attackPointOffset;
        Collider2D allHitObjects = Physics2D.OverlapBox(_attackPoint.position, new Vector2(_attackRange, _attackRange * .75f), _playerLayer); 
        
        if (allHitObjects != null)
        {
            allHitObjects.GetComponent<PlayerManager>().TakeDamage(_enemy.GetPhysicalDamage());
        }
    }

    public void DealDamage(Rigidbody2D player)
    {
        player.GetComponent<PlayerManager>().TakeDamage(_enemy.GetPhysicalDamage());
    }

    //ples delete me
    private void OnDrawGizmosSelected()
    {
        if(_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireCube(_attackPoint.position, new Vector2(_attackRange, _attackRange * .75f));
    }

    public float GetAggroRange()
    {
        return _aggroRange;
    }

    public float GetAttackRange()
    {
        return _attackRange;
    }
}
