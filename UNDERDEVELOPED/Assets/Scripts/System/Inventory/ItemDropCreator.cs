using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDropCreator : MonoBehaviour
{
    private static Transform _dropsGamObjectTransform;
    // private Transform _itemDropsGameObject;
    // public static ItemDropCreator _instance;

    // private void Awake()
    // {
    //     if (_instance != null && _instance != this)
    //     {
    //         Destroy(this);
    //         return;
    //     }

    //     _instance = this;
    // }

    private void Start()
    {
        _dropsGamObjectTransform = transform;
    }
    
    // Create new itemdrop gameobject inside the Drops (this transform)
    // Get the world positon of the droptransform (player or enemy) and set the drop position
    // And instantiate all required components
    public static void CreateItemDrop(Vector3 dropTransform, Item item)
    {
        // Vector3 position;
        // Quaternion rotation;
        // dropTransform.GetPositionAndRotation(out position, out rotation);

        Debug.Log("ItemDropCreator: " + item._name);
        GameObject itemDrop = new GameObject(item._name, typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(ItemDropController));
        itemDrop.GetComponent<ItemDropController>()._item = item;
        itemDrop.transform.SetParent(_dropsGamObjectTransform);
        // itemDrop.transform.SetPositionAndRotation(position, rotation);
        itemDrop.transform.position = dropTransform;

        Debug.Log("IteDropCreator: New GameObject is created");
    }
}
