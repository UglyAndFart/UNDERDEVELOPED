using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackPoint;
    [SerializeField]
    private TopDownMovementController _topDownMovementController;

    public void OnAttackEnd()
    {
        _attackPoint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D enemyHit)
    {
        Debug.Log("AttackController: " + enemyHit.name);
        if (enemyHit.CompareTag("Enemy"))
        {
            _topDownMovementController.AddEnemyToArray(enemyHit);
        }
    }
}
