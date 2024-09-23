using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField]
    private string _name, _map;
    
    [Header("Player Stats")]
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _health, _maxStamina, _stamina, _moveSpeed,
    _physicalDamage, _magicDamage, _dashDistance, _dashDuration, _dashCooldown,
    _dashCost, _staminaRegenRate, _staminaRecoveryBufferTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerData()
    {
        PlayerData playerData = SaveSystemManager.LoadPlayer();
        Vector3 position;
        
        _health = playerData._health;
        _moveSpeed = playerData._moveSpeed;
        _stamina = playerData._stamina;
        _physicalDamage = playerData._physicalDamage;
        _magicDamage = playerData._magicDamage;
        _dashDistance = playerData._dashDistance;
        _dashCooldown = playerData._dashCooldown;
        _dashDuration = playerData._dashDuration;
        _staminaRecoveryBufferTime = playerData._staminaRecoveryBufferTime;

        _map = playerData._map;

        position.x = playerData.position[0];
        position.y = playerData.position[1];
        position.z = playerData.position[2];
        transform.position = position;
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

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxStamina()
    {
        return _maxStamina;
    }

    public float GetStamina()
    {
        return _stamina;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
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

    public float GetDashCost()
    {
        return _dashCost;
    }

    public float GetStaminaRecoveryBufferTime()
    {
        return _staminaRecoveryBufferTime;
    }

    public float GetStaminaRegenRate()
    {
        return _staminaRegenRate;
    }

    public string GetCurrentMap()
    {
        return _map;
    }

    public string GetName()
    {
        return _name;
    }
}
