using System.Collections;
using UnityEngine;

public class BackgroundCameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Transform[] _cameraPoints;
    [SerializeField]
    private float _panSpeed;
    private Vector3 _targetPosition;

    // [SerializeField]
    // private float _panSpeed, _distance, _switchDelay;
    // private Vector2 _currentPoint, _targetPosition;

    private void Start()
    {
        if (_cameraPoints.Length > 0)
        {
            _mainCamera.transform.position = _cameraPoints[0].position;
            _targetPosition = _cameraPoints[1].position;
        }
    }

    private void Update()
    {
        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _targetPosition, _panSpeed * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position, _targetPosition) < 0.01f)
        {
            _targetPosition = _targetPosition == _cameraPoints[0].position ? _cameraPoints[1].position : _cameraPoints[0].position;
        }

        // PanCamera();
    }

    // private void PanCamera()
    // {
    //     _mainCamera.transform.position = Vector2.Lerp(_mainCamera.transform.position, _targetPosition, _panSpeed * Time.deltaTime);

    //     if (Vector2.Distance(_mainCamera.transform.position, _currentPoint) >= _distance * .9)
    //     {
    //         _targetPosition = _currentPoint - new Vector2(_distance, 0);
    //     }
    // }

    // private IEnumerator RandomizerTimer()
    // {
    //     yield return new WaitForSeconds(_switchDelay);

    //     if (Random.Range(0, 1) == 1)
    //     {
    //         RandomizePoints();
    //     }
    // }

    // private void RandomizePoints()
    // {
    //     // _mainCamera.transform.position = _cameraPoints[Random.Range(0, _cameraPoints.Length)].transform.position;
    //     _currentPoint = _cameraPoints[Random.Range(0, _cameraPoints.Length - 1)].position;
    //     Debug.Log("BackgroundCameraController: Randomized" + _currentPoint);
    //     _targetPosition = _currentPoint + new Vector2(_distance, 0);
    // }
}
