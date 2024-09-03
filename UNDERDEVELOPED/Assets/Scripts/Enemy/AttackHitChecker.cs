using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitChecker : MonoBehaviour
{
    [SerializeField]
    private Collider2D _attackDimension;
    [SerializeField]
    private EnemyManager _enemyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetType() != typeof(CapsuleCollider2D))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            _enemyManager.GetComponent<EnemyManager>().Attack();
        }
    }
}
