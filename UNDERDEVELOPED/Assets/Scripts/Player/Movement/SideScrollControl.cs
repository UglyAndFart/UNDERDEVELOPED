using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollControl : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private float speed;

    void Update()
    {
        Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
            }

            GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
}
