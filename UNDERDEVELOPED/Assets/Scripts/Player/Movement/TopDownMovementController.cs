using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    public static TopDownMovementController _instance;
    private PlayerManager _playerManager;
    private GameObject _attackPoint;
    private Vector2 _direction;
    private Vector2 _facingDirection;
    private bool _flipSprite, _activeUI = false;
    private List<Collider2D> _enemyHits;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        HUDManager.OnUIOpen += DisablePlayerControls;
        HUDManager.OnUIClose += EnablePlayerControls;

        _playerManager = PlayerManager._instance;
        _attackPoint = CharacterPrefabLoader._instance.GetCurrentCharacter().transform.Find("Attack Pivot").gameObject;
        _flipSprite = false;
        _facingDirection = Vector2.right;
        _enemyHits = new List<Collider2D>();
    }

    private void Update()
    {   
        if (_activeUI)
        {
            Debug.Log("TopDownMovementController: a UI is Active");
            return;
        }

        Movement();
        Dash();
        Attack();
    }

    private void FixedUpdate()
    {
        Stamina();
    }

    private void Movement()
    {
        _direction = Vector2.zero;
        
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");

        if (_direction.x < 0)
        {
            _flipSprite = true;
        }
        else
        {
            _flipSprite = false;
        }

        // if (Input.GetKey(KeyCode.A))
        // {
        //     _direction.x = -1;
        //     _flipSprite = true;
        // }
        // else if (Input.GetKey(KeyCode.D))
        // {
        //     _direction.x = 1;
        //     _flipSprite = false;
        // }

        // if (Input.GetKey(KeyCode.W))
        // {
        //     _direction.y = 1;
        // }
        // else if (Input.GetKey(KeyCode.S))
        // {
        //     _direction.y = -1;
        // }

        if (_direction.x != 0 || _direction.y != 0)
        {
            _facingDirection = _direction.normalized;
            _playerManager.FlipSpriteX(_flipSprite);
            _playerManager.PlayerAnimationMoving(true);
            _playerManager.PlayerMovePosition(_direction);
        }
        else
        {
            _playerManager.PlayerAnimationMoving(false);
        }
    }

    private void Dash()
    {
        // if (_playerManager.)
        // {

        // }

        if (Input.GetButtonDown("Dash"))
        {
            DashTrigger();
        }
    }

    private void DashTrigger()
    {
        _playerManager.PlayerDash(_facingDirection);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _attackPoint.SetActive(true);

        }

        if (_enemyHits != null)
        {
            _playerManager.DealDamage(_enemyHits);
            _enemyHits = new List<Collider2D>();
        }
    }

    public void AddEnemyToArray(Collider2D enemyHit)
    {
        _enemyHits.Add(enemyHit);
    }

    private void Stamina()
    {
        _playerManager.RegenStamina();
    }

    // public void UseStamina()
    // {
        
    // }

    // private void StartStaminaRegen()
    // {
    //     _staminaRegenation = true;
    // }

    // private void StopStaminaRegen()
    // {
    //     _staminaRegenation = false;
        
    // }

    public void SetPosition(Vector3 newPosition)
    {
        CharacterPrefabLoader._instance.GetCurrentCharacter().transform.position = newPosition;
        // transform.localPosition = transform.TransformPoint(newPosition); 
    }
    
    public Vector3 GetPosition()
    {
        return CharacterPrefabLoader._instance.GetCurrentCharacter().transform.position;
    }

    public void EnablePlayerControls()
    {
        _activeUI = false;
    }

    public void DisablePlayerControls()
    {
        _activeUI = true;
    }

    // private void SetNewGamePosition()
    // {
    //     if (SpawnPointManager._instance._newGame)
    //     {
    //         Debug.Log("TopDownMovementController: set tot newgame Main");
    //         SetPosition(SpawnPointManager._instance.GetSpawnPoint("Main").transform.position);
    //     }
    // }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("TopDownMovementContorllers: On load triggered");
        if (scene.name == "South Forest")
        {
            Debug.Log("TopDownMovementController: on South Forest load");

            if (SpawnPointManager._instance._newGame)
            {
                Debug.Log("TopDownMovementController: set tot newgame Main");
                SetPosition(SpawnPointManager._instance.GetSpawnPoint("Main").transform.position);
            }

            // Debug.Log("OnSceneLoaded from TopDownMovement");
            // //transform.position = new Vector3(658.77f, 289.22f, 0);
            // GameObject spawnpoint = PlayerGameObjectFinder.FindSpawnPoint("Player Deault Spawnpoint");
            // transform.position = spawnpoint.transform.position;
        }
        // else if (scene.name == "Realm")
        // {
        //     SetPosition(SpawnPointManager._instance.GetSpawnPoint("Main").gameObject.transform.position);
        //     Debug.Log("Realm: Position is set to Realm Main");
        // }
        // else if (scene.name == "Realm")
        // {
        //     GameObject spawnpoint = PlayerGameObjectFinder.FindSpawnPoint("SpawnPoint");
        //     transform.position = spawnpoint.transform.position;
        // }
        
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        HUDManager.OnUIOpen -= DisablePlayerControls;
        HUDManager.OnUIClose -= EnablePlayerControls;
    }
}
