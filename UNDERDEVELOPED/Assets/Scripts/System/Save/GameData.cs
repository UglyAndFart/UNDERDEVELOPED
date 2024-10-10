using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float _health, _stamina, _moveSpeed, _dashDistance, _dashCooldown,
    _dashDuration, _physicalDamage, _magicDamage, _staminaRecoveryBufferTime;
    public string _name, _map, _characterType;
    public float[] position;

    public List<List<bool>> _bossesState, _chestsState, _potsState, _cutscenesState;

    public GameData(Player player, GameEnvironment environment)
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
        _name = player.GetPlayerName();
        _characterType = player.GetCharacterType();

        position = new float[3];

        position[0] = player.GetPlayerPosition().x;
        position[1] = player.GetPlayerPosition().y;
        position[2] = player.GetPlayerPosition().z;

        _bossesState = environment.GetBossesStates();
        _chestsState = environment.GetChestsStates();
        _potsState = environment.GetPotsStates();
        _cutscenesState = environment.GetCutsceneStates();
    }
}
