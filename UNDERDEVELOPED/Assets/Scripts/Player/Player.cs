using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _health, _stamina, _moveSpeed, _physicalDamage, _magicDamage, _dashDistance, _dashDuration, _dashCooldown;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetPlayerData()
    {
        PlayerData playerData = SaveSystemManager.LoadPlayer();
        Vector3 position;
        
        _health = playerData.health;
        position.x = playerData.position[0];
        position.y = playerData.position[1];
        position.z = playerData.position[2];
        transform.position = position;
        _moveSpeed = playerData.moveSpeed;
        _stamina = playerData.stamina;
        _physicalDamage = playerData.physicalDamage;
        _magicDamage = playerData.magicDamage;
        _dashDistance = playerData.dashDistance;
        _dashCooldown = playerData.dashCooldown;
        _dashDuration = playerData.dashDuration;
    }
    public void AddHealth(float health)
    {
        _health += health;
    }

    public void DeductHealth(float health)
    {
        _health -= health;
    }

    public void AddStamina(float stamina)
    {
        _stamina += stamina;
    }

    public void DeductStamina(float stamina)
    {
        _stamina -= stamina;
    }

    public void Die()
    {

    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    public float GetStamina()
    {
        return _stamina;
    }

    public float GetPhysicalDamage()
    {
        return _physicalDamage;
    }

    public float GetMagicDamage()
    {
        return _magicDamage;
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
