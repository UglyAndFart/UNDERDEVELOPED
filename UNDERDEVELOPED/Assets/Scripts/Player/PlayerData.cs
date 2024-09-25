using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float _health, _stamina, _moveSpeed, _dashDistance, _dashCooldown,
    _dashDuration, _physicalDamage, _magicDamage, _staminaRecoveryBufferTime;
    public string _name, _map, _characterType;
    public float[] position;

    public PlayerData(Player player)
    {
        _health = player.GetHealth();        
        _stamina = player.GetStamina();
        _moveSpeed = player.GetMoveSpeed();
        _dashDistance = player.GetDashDistance();
        _dashCooldown = player.GetDashCooldown();
        _dashDuration = player.GetDashDuration();
        _physicalDamage = player.GetPhysicalDamage();
        _magicDamage = player.GetMagicDamage();
        _staminaRecoveryBufferTime = player.GetStaminaRecoveryBufferTime();

        _map = player.GetCurrentMap();
        _name = player.GetName();

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
