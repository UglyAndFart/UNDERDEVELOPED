using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;

    private Player _player;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _moveSpeed, _dashDistance, _dashDuration, _dashCooldown,
    _dashCost, _staminaRegenRate, _staminaRecoveryBufferTime = 0;
    private bool _canDash = true, _isDashing = false, _isAlive = true;
    private string _previousMap;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        Debug.Log("PlayerManager Started");

        _player = Player._instance;

        if (_player == null)
        {
            Debug.LogWarning("PlayerManager: Player Not Found");
        }
        else
        {
            Debug.Log("PlayerManager: Player Found");
        }

        SetupComponents();
    }

    public void SetupComponents()
    {
        GameObject characterObject = CharacterPrefabLoader._instance.GetCurrentCharacter();

        if (characterObject != null)
        {
            Debug.Log("PlayerManager: Found characterObject");
        }
        else
        {
            Debug.LogWarning("PlayerManager: characterObject Not Found");
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        _rigidBody2D = characterObject.GetComponent<Rigidbody2D>();
        _animator = characterObject.GetComponent<Animator>();
        _spriteRenderer = characterObject.GetComponent<SpriteRenderer>();;
        _moveSpeed = _player.GetMoveSpeed();
        _dashDistance = _player.GetDashDistance();
        _dashDuration = _player.GetDashDuration();
        _dashCooldown = _player.GetDashCooldown();
        _dashCost = _player.GetDashCost();
        _staminaRegenRate = _player.GetStaminaRegenRate();
    }

    //updates the PlayerPostion in player every time the player is moving
    public void PlayerMovePosition(Vector2 direction)
    {
        if (!_isDashing)
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + direction * _moveSpeed * Time.deltaTime);
            _player.SetPlayerPosition(TopDownMovementController._instance.GetPosition());
        }
    }

    public void FlipSpriteX(bool flipSprite)
    {
        _spriteRenderer.flipX = flipSprite;
    }

    public void PlayerAnimationMoving(bool moving)
    {
        _animator.SetBool("Moving", moving);
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("Hurt");
        _player.DeductHealth(damage);

        if ((_player.GetHealth() - damage) <= 0 && _isAlive)
        {
            _isAlive = false;
            _animator.SetBool("Alive", false);
            OnDeath();
            return;
        }
    }

    public void DealDamage(List<Collider2D> enemyHits)
    {
        // HashSet<Collider2D> enemyHitsSet = new HashSet<Collider2D>(enemyHits);
        foreach (Collider2D enemyHit in enemyHits)
        {
            enemyHit.GetComponent<EnemyManager>().TakeDamage(_player.GetPhysicalDamage());
        }
    }

    public void PlayerAnimationDeath()
    {
        _animator.SetBool("Alive", false);
    } 

    public void RegenStamina()
    {
        if (_staminaRecoveryBufferTime <= 0 && _player.GetStamina() < _player.GetMaxStamina())
        {
            Debug.Log("Stamina Regenerating");
            _player.AddStamina(_staminaRegenRate);
        }
    }

    public void PlayerDash(Vector2 facingDirection)
    {
        if(_player.GetStamina() >= _dashCost && _canDash)
        {
            StartCoroutine(PerformDash(facingDirection));
            StartCoroutine(StopStaminaRegen());
        }
    }

    public IEnumerator PerformDash(Vector2 facingDirection)
    {
        _canDash = false;
        _isDashing = true;

        _player.DeductStamina(_dashCost);

        Vector2 dashVector = facingDirection * _dashDistance;
        float elapsedTime = 0f;
        
        //Debug.Log("Dashing");
        
        while (elapsedTime < _dashDuration)
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + dashVector * Time.deltaTime / _dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isDashing = false;

        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    public IEnumerator StopStaminaRegen()
    {
        _staminaRecoveryBufferTime = _player.GetStaminaRecoveryBufferTime();
        
        while (_staminaRecoveryBufferTime > 0)
        {
            _staminaRecoveryBufferTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void OnDeath()
    {
        _player.GetComponent<TopDownMovementController>().enabled = false;
        HUDManager._instance.OpenDeathScreen();
        StartCoroutine(RespawnTimer());
    }

    public bool GetDashing()
    {
        return _isDashing;
    }

    public bool GetCanDash()
    {
        return _canDash;
    }

    public void SetPreviousMap(string previousMap)
    {
        _previousMap = previousMap;
    }

    public string GetPreviousMap()
    {
        return _previousMap;
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "South Forest")
        {
            _player.SetCurrentMap(scene.name);
        }
        else if (scene.name == "SF 2")
        {
            _player.SetCurrentMap(scene.name);
        }
        else if (scene.name == "Outside Town")
        {
            _player.SetCurrentMap(scene.name);
        }
        else if (scene.name == "Town")
        {
            _player.SetCurrentMap(scene.name);
        }
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(5);
        HUDManager._instance.CloseDeathScreen();
        _player.Die();
        _player.GetComponent<TopDownMovementController>().enabled = true;
        _animator.SetBool("Alive", true);
        _isAlive = true;
        StopCoroutine(RespawnTimer());
    }

    // Set the previousMap value with the current scene name
    private void OnDestroy()
    {
        // string currentScene = SceneManager.GetActiveScene().name;
        
        // if (currentScene == "South Forest")
        // {
        //     _previousMap = currentScene;
        // }
        // else if (currentScene == "SF 2")
        // {
        //     _previousMap = currentScene;
        // }
        // else if (currentScene == "Outside Town")
        // {
        //     _previousMap = currentScene;
        // }
            
        SceneManager.sceneLoaded -= OnSceneLoad;    
    }
}
