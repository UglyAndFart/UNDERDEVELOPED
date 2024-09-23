using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPivot : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private Vector3 difference;

    // private void FixedUpdate()
    // {
    //     difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    //     difference.Normalize();
    //     float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    //     transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

    //     if (rotationZ < -90 || rotationZ > 90)
    //     {
    //         if (_player.transform.eulerAngles.y == 0)
    //         {
    //             transform.rotation = Quaternion.Euler(180f, 0f, -rotationZ);
    //         }
    //         else if (_player.transform.eulerAngles.y == 180)
    //         {
    //             transform.localRotation = Quaternion.Euler(180f, 180f, -rotationZ);
    //         }
    //     } 
    // }

    private void OnEnable()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if (_player.transform.eulerAngles.y == 0)
            {
                transform.rotation = Quaternion.Euler(180f, 0f, -rotationZ);
            }
            else if (_player.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180f, 180f, -rotationZ);
            }
        }
    }
}
