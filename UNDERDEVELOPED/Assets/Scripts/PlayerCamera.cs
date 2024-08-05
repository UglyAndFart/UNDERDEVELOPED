using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset = new Vector3(0,1,0);

    private Vector3 targetPos;

    private void Start()
    {
        if (target == null) return;
        
        transform.position = target.position;
    }

    private void Update()
    {
        if (target == null) return;

        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}
