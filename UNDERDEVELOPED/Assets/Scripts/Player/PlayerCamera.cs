using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private float lerpSpeed = 1.0f;

    private void Start()
    {
        if (mainCamera == null) return;
        
        mainCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }

    private void Update()
    {
        if (mainCamera == null) return;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3 (transform.position.x, transform.position.y, mainCamera.transform.position.z), lerpSpeed * Time.deltaTime);
    }
}
