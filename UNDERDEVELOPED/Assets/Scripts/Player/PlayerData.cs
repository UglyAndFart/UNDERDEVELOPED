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
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        
        stamina = player.stamina;
        moveSpeed = player.moveSpeed;
        dashDistance = player.dashDistance;
        dashCooldown = player.dashCooldown;
        dashDuration = player.dashDuration;
        physicalDamage = player.physicalDamage;
        magicDamage = player.magicDamage;
    }
}
