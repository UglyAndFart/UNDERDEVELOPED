using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float _health, _moveSpeed, _physicalDamage, _magicDamage;
    public float[] _position;

    public EnemyData(Enemy enemy)
    {
        _health = enemy.GetHealth();
        _moveSpeed = enemy.GetMoveSpeed();
        _physicalDamage = enemy.GetPhysicalDamage();
        _magicDamage = enemy.GetMagicDamage();

        _position = new float[3];
        _position[0] = enemy.transform.position.x;
        _position[1] = enemy.transform.position.y;
        _position[2] = enemy.transform.position.z;

    }
}
