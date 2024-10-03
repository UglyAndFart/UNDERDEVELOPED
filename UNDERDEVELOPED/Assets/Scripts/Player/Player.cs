using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public static Player _instance;

    [SerializeField]
    private string _name, _map, _characterType;
    private Vector3 _playerPosition;
    private Item[] _equipments;

    [Header("Player Stats")]
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _health, _maxStamina, _stamina, _moveSpeed,
    _physicalDamage, _magicDamage, _dashDistance, _dashDuration, _dashCooldown,
    _dashCost, _staminaRegenRate, _staminaRecoveryBufferTime;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
    }

    public void SetPlayer()
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

        _name = playerData._name;
        _map = playerData._map;
        _characterType = playerData._characterType;

        position.x = playerData.position[0];
        position.y = playerData.position[1];
        position.z = playerData.position[2];

        _playerPosition = position;

        //TopDownMovementController._instance.SetPosition(_playerPosition);
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

    public void SetCurrentMap(string map)
    {
        _map = map;
    }

    public string GetCurrentMap()
    {
        return _map;
    }

    public void SetPlayerName(string name)
    {
        _name = name;
    }
    
    public string GetPlayerName()
    {
        return _name;
    }

    public void SetChacterType(string characterType)
    {
        _characterType = characterType;
    }

    public string GetCharacterType()
    {
        return _characterType;
    }
    
    public void SetEquipItem(Item item, int index)
    {
        _equipments[index] = item;
    }

    public Item[] GetCurrentEquipItem()
    {
        return _equipments;
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerPosition;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        _playerPosition = position;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
