using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private TextMeshProUGUI hp;

    [SerializeField]
    public float health, moveSpeed, stamina, physicalDamage, magicDamage, dashDistance, dashCooldown, dashDuration;

    void Start()
    {
        SetPlayerData();
    }

    void Update()
    {
        //UpdateHPBar();
    }
    public void SetPlayerData()
    {
        PlayerData playerData = SaveSystemManager.LoadPlayer();
        Vector3 position;
        
        health = playerData.health;
        position.x = playerData.position[0];
        position.y = playerData.position[1];
        position.z = playerData.position[2];
        transform.position = position;
        moveSpeed = playerData.moveSpeed;
        stamina = playerData.stamina;
        physicalDamage = playerData.physicalDamage;
        magicDamage = playerData.magicDamage;
        dashDistance = playerData.dashDistance;
        dashCooldown = playerData.dashCooldown;
        dashDuration = playerData.dashDuration;
    }

    public void UpdateHPBar()
    {
        hp.text = $"{health}";
    }
}
