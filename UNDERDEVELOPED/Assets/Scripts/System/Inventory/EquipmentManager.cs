using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public static OnEquipmentChanged onEquipmentChanged;
    public static EquipmentManager _instance;
    private Equipment[] _currentEquipment;
    private Inventory _inventory;

    private void Awake()
    {
        if (_instance != null&& _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
    }

    private void Start()
    {
        _inventory = Inventory._instance;

        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[equipSlots];
    }

    public void EquipItem(Equipment newEquipment)
    {
        int equipType = (int) newEquipment._equipmentSlot;
        Equipment oldItem = null;

        if (_currentEquipment[equipType] != null)
        {
            oldItem = _currentEquipment[equipType];
            _inventory.AddItem(oldItem);
        }

        _currentEquipment[equipType] = newEquipment; 
        onEquipmentChanged?.Invoke(_currentEquipment[equipType], oldItem);
    }

    // When slot isnt empty remove the Equipment from array and add back to Inventory
    public void UnequipItem(int equipType)
    {
        Debug.Log("EquipmentManager: " + equipType);

        if (_currentEquipment[equipType] != null)
        {
            Equipment oldItem = _currentEquipment[equipType];
            _inventory.AddItem(oldItem);
            _currentEquipment[equipType] = null;
            onEquipmentChanged?.Invoke(null, oldItem);
        }
    }

    public Equipment[] GetCurrentEquipments()
    {
        // List<Equipment> equipmentList = new List<Equipment>();

        // foreach (Equipment equipment in _currentEquipment)
        // {
        //     if (equipment != null)
        //     {
        //         equipmentList.Add(equipment);
        //     }
        // }

        // return equipmentList.ToArray();
        return _currentEquipment;
    }
}
