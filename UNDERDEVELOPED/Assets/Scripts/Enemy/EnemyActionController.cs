using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private int attackCount = 1;

    private EnemyManager _enemyManager;
    private Vector2 _currentPosition;
    private float _distance, _aggroRange, _attackRange;
    private bool _attacking = false;

    private void Start()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _attackRange = _enemyManager.GetAttackRange();
        _aggroRange = _enemyManager.GetAggroRange();
    }

    private void Update()
    {
        if(_attacking)
        {
            return;
        }

        CheckMove();

        if (_distance <= _attackRange*.75f)
        {
            CheckAttack();
        }
    }

    private void CheckMove()
    {

        _currentPosition = transform.position;
        _distance = Vector2.Distance(_currentPosition, player.position);

        if (_distance > _aggroRange)
        {
            _enemyManager.EnemyAnimationMoving(false);
            return;
        }

        Vector2 direction = player.position - _currentPosition;
        direction.Normalize();
        _enemyManager.FlipSpriteX(direction);
        _enemyManager.EnemyAnimationMoving(true);
        _enemyManager.EnemyMovePosition(direction, _currentPosition);
    }

    private void CheckAttack()
    {
        if(attackCount == 1)
        {
            _enemyManager.AttackAnimation("Attack1");
            return;
        }

        int attackNumber = UnityEngine.Random.Range(1, attackCount + 1);
        _enemyManager.AttackAnimation($"Attack{attackNumber}");
    }

    public void EnemyAttackStart()
    {
        _attacking = true;
    }

    public void EnemyAttackEnd()
    {
        _attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            _enemyManager.DealDamage(player);
        }    
    }

    
}
