using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject _player;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _player = PlayerGameObjectFinder.FindPlayerPrefab();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        SetPlayerToVirtualCam();
    }

    private void SetPlayerToVirtualCam()
    {
        _virtualCamera.Follow = _player.transform;
    }
    // [SerializeField]
    // private GameObject mainCamera;
    // [SerializeField]
    // private float lerpSpeed = 1.0f;

    // private void Start()
    // {
    //     if (mainCamera == null) return;
        
    //     mainCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, mainCamera.transform.position.z);
    // }

    // private void Update()
    // {
    //     if (mainCamera == null) return;

    //     mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3 (transform.position.x, transform.position.y, mainCamera.transform.position.z), lerpSpeed * Time.deltaTime);
    // }
}
