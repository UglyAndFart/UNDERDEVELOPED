using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _health, _physicalDamage, _magicDamage, _aggroRange, _moveSpeed, _attackRange, _attackSpeed;    

    private void Start()
    {
        
    }
    
    public void DeductHealth(float damage)
    {
        _health -= damage;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetPhysicalDamage()
    {
        return _physicalDamage;
    }

    public float GetMagicDamage()
    {
        return _magicDamage;
    }
    public float GetAggroRange()
    {
        return _aggroRange;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    public float GetAttackRange()
    {
        return _attackRange;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
}
