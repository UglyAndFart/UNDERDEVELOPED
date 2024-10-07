using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideScrollControl : MonoBehaviour
{   
    [SerializeField]
    private GameObject _activeTriggerObject;
    private PlayerManager _playerManager;
    private GameObject _currentCharacter;
    private Vector2 _direction;
    // private Vector2 _facingDirection;
    private bool _flipSprite, _activeUI;

    private void Awake()
    {
        _playerManager = PlayerManager._instance;
        TopDownMovementController._instance.enabled = false;
        _currentCharacter = CharacterPrefabLoader._instance.GetCurrentCharacter();
        _currentCharacter.GetComponent<Rigidbody2D>().gravityScale = 3; 
    }
    private void Start()
    {
        SetPosition(SpawnPointManager._instance.GetSpawnPoint("Main").transform.position);
        _activeTriggerObject.GetComponent<ActiveStateTrigger>().SetBehaviour(TopDownMovementController._instance); 
        
        // SceneManager.sceneLoaded += OnSceneLoaded;
        HUDManager.OnUIOpen += DisablePlayerControls;
        HUDManager.OnUIClose += EnablePlayerControls;
    }

    private void Update()
    {
        if (_activeUI)
        {
            return;
        }

        _direction = Vector2.zero;

        _direction.x = Input.GetAxis("Horizontal");    

        if (_direction.x < 0)
        {
            _flipSprite = true;
        }
        else
        {
            _flipSprite = false;
        }

        if (_direction.x != 0)
        {
            // _facingDirection = _direction.normalized;
            _playerManager.FlipSpriteX(_flipSprite);
            _playerManager.PlayerAnimationMoving(true);
            _playerManager.PlayerMovePosition(_direction);
        }
        else
        {
            _playerManager.PlayerAnimationMoving(false);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        _currentCharacter.transform.position = newPosition;
    }

    public void EnablePlayerControls()
    {
        _activeUI = false;
    }

    public void DisablePlayerControls()
    {
        _activeUI = true;
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     SetPosition(SpawnPointManager._instance.GetSpawnPoint("Main").transform.position);
    // }

    private void OnDestroy()
    {
        // _currentCharacter.GetComponent<Rigidbody2D>().gravityScale = 0; 
        // TopDownMovementController._instance.enabled = true;
        // SceneManager.sceneLoaded -= OnSceneLoaded;
        HUDManager.OnUIOpen -= DisablePlayerControls;
        HUDManager.OnUIClose -= EnablePlayerControls;
    }
}
