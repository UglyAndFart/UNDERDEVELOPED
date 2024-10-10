using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory _instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public List<Item> _items = new List<Item>();
    public List<Equipment> _equipments = new List<Equipment>();
    private int _inventoryCapacity = 36;

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
        if (item is Equipment equipment)
        {
            if (equipment._defaultItem)
            {
                return false;
            }

            if (_equipments.Count >= _inventoryCapacity)
            {
                Debug.LogWarning("Full Inventory");
                return false;
            }

            _equipments.Add(equipment);

            onItemChangedCallBack?.Invoke();
            return true;
        }

        if (item._defaultItem)
        {
            return false;
        }

        if (_items.Count >= _inventoryCapacity)
        {
            Debug.LogWarning("Full Inventory");
            return false;
        }
        
        _items.Add(item);

        onItemChangedCallBack?.Invoke();
        
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (item is Equipment equipment)
        {
            _equipments.Remove(equipment);
        }
        else if (item is Item)
        {
            _items.Remove(item);
        }

        onItemChangedCallBack?.Invoke();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
