using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUpdater : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _healthFill;
    [SerializeField]
    private Slider _staminaFill;
    [SerializeField]
    private HUDManager _hudmanager;

    private void Start()
    {
        SetStatMaxValue();
    }

    private void Update()
    {
        UpdatePlayerStat();

        if (_player.GetHealth() <= 0)
        {
            _hudmanager.OpenDeathScreen();
        }
        
        Debug.Log($"HP: {_player.GetHealth()}");
    }

    private void SetStatMaxValue()
    {
        _healthFill.maxValue = _player.GetMaxHealth();
        _staminaFill.maxValue = _player.GetMaxStamina();
    }

    private void UpdatePlayerStat()
    {
        _healthFill.value = _player.GetHealth();
        _staminaFill.value = _player.GetStamina();
    }
}
