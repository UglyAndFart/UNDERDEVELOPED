using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField]
    private float _itemPickupRadius = 2f, _travelSpeed;
    
    [SerializeField]
    private Item _item;

    private CircleCollider2D _circleCollider2D;
    //private bool _pickedUp;

    private void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = _itemPickupRadius;
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Interact"))
        {
            OnItemPickup();
        }
    }

    private void OnItemPickup()
    {
        Debug.Log("Name:" + _item);
        bool pickedUp = Inventory._instance.AddItem(_item);

        if (pickedUp)
        {
            Destroy(this);
        }
    }
}
