using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropController : MonoBehaviour
{
    [SerializeField]
    private float _itemPickupRadius = 2f, _travelSpeed = 0;
    [SerializeField]
    private Item _item;
    private CircleCollider2D _circleCollider2D;
    private bool _playerInrange;

    private void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = _itemPickupRadius;
        GetComponent<SpriteRenderer>().sprite = _item._icon;
    }

    private void Update()
    {
        if (_travelSpeed <= 0)
        {
            if (_playerInrange && Input.GetButtonDown("Interact"))
            {
                OnItemPickup();
            }
        }
        else if (_travelSpeed > 0)
        {
            transform.position = Vector2.Lerp(transform.position, Player._instance.GetPlayerPosition(), _travelSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, Player._instance.GetPlayerPosition()) < .2f)
            {
                OnItemPickup();
            }
        }
        
    }

    //Destroy the itemdrop gameobject when it is picked
    private void OnItemPickup()
    {
        if (Inventory._instance.AddItem(_item))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerInrange = true;
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player") && Input.GetButtonDown("Interact"))
    //     {
    //         OnItemPickup();
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_playerInrange)
        {
            return;
        }

        _playerInrange = false;
    }
}
