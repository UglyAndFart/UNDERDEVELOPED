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
            return;
        }

        _instance = this;
    }

    public void SetPlayer()
    {
        GameData gameData = SaveSystemManager.LoadGame();
        Vector3 position;
        
        _health = gameData._health;
        _moveSpeed = gameData._moveSpeed;
        _stamina = gameData._stamina;
        _physicalDamage = gameData._physicalDamage;
        _magicDamage = gameData._magicDamage;
        _dashDistance = gameData._dashDistance;
        _dashCooldown = gameData._dashCooldown;
        _dashDuration = gameData._dashDuration;
        _staminaRecoveryBufferTime = gameData._staminaRecoveryBufferTime;

        _name = gameData._name;
        _map = gameData._map;
        _characterType = gameData._characterType;

        position.x = gameData.position[0];
        position.y = gameData.position[1];
        position.z = gameData.position[2];

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
