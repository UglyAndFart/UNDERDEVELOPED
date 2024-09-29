using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory _instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public List<Item> _items = new List<Item>();
    private int _inventoryCapacity = 81;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Oh no, you fck up at Inventory _instance");
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public bool AddItem(Item item)
    {
        if (item.defaultItem)
        {
            return false;
        }

        if (_items.Count >= _inventoryCapacity)
        {
            Debug.LogWarning("Full Inventory");
            return false;
        }
        
        _items.Add(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        
        return true;
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
        
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
