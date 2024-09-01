using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health, stamina, moveSpeed, dashDistance, dashCooldown, dashDuration, physicalDamage, magicDamage;
    public float[] position;

    public PlayerData(Player player)
    {
        health = player.GetHealth();

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        
        stamina = player.GetStamina();
        moveSpeed = player.GetMoveSpeed();
        dashDistance = player.GetDashDistance();
        dashCooldown = player.GetDashCooldown();
        dashDuration = player.GetDashDuration();
        physicalDamage = player.GetPhysicalDamage();
        magicDamage = player.GetMagicDamage();
    }
}
