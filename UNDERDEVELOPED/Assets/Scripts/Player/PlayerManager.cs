using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
    private bool _canDash = true, _dashing = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
    }

    private void Start()
    {
        Debug.Log("PlayerManager Started");

        _player = Player._instance;
        GameObject characterObject = CharacterPrefabLoader._instance.GetCurrentCharacter();

        if (characterObject != null)
        {
            Debug.Log("Found characterObject");
        }
        else
        {
            Debug.LogWarning("characterObject Not Found");
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

    public void PlayerMovePosition(Vector2 direction)
    {
        if (!_dashing)
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + direction * _moveSpeed * Time.deltaTime);
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
        if ((_player.GetHealth() - damage) <= 0)
        {
            _animator.SetBool("Alive", false);
        }

        _animator.SetTrigger("Hurt");
        _player.DeductHealth(damage);
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
        _dashing = true;

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

        _dashing = false;

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
    }

    public bool GetDashing()
    {
        return _dashing;
    }

    public bool GetCanDash()
    {
        return _canDash;
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _player.SetCurrentMap(scene.name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;    
    }
}
