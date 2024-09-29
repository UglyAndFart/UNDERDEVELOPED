using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUpdater : MonoBehaviour
{
    public static PlayerStatUpdater _instance;
    private Player _player;
    [SerializeField]
    private Slider _healthFill;
    [SerializeField]
    private Slider _staminaFill;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }

        _instance = this;


        // if (_player != null)
        // {
        //     Debug.Log("PlayerStatusUpdater: Player Found");
        // }
        // else
        // {
        //     Debug.Log("PlayerStatusUpdater: Player Not Found");
        // }
    }

    private void Start()
    {
        _player = Player._instance;
        HUDManager._instance.OpenPlayerStatus();
        SetStatMaxValue();
    }

    private void Update()
    {
        UpdatePlayerStat();

        if (_player.GetHealth() <= 0)
        {
            HUDManager._instance.OpenDeathScreen();
        }
        
        Debug.Log($"HP: {_player.GetHealth()}");
    }

    private void SetStatMaxValue()
    {
        Debug.Log("PlayerStatusUpdater: " + _player.GetMaxHealth());
        Debug.Log("PlayerStatusUpdater: " + _healthFill.maxValue);
        _healthFill.maxValue = _player.GetMaxHealth();
        _staminaFill.maxValue = _player.GetMaxStamina();
    }

    private void UpdatePlayerStat()
    {
        _healthFill.value = _player.GetHealth();
        _staminaFill.value = _player.GetStamina();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
