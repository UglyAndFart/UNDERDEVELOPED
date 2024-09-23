using UnityEditor.Search;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform _slotsHolder;
    
    private Inventory _inventory;
    private InventorySlotController[] _inventorySlots;
    
    private void Start()
    {
        _inventory = Inventory._instance;
        _inventory.onItemChangedCallBack += UpdateInventoryUI;

        _inventorySlots = _slotsHolder.GetComponentsInChildren<InventorySlotController>();
    }

    private void Update()
    {
        
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < _inventory._items.Count)
            {
                _inventorySlots[i].AddItem(_inventory._items[i]);
            }
            else
            {
                _inventorySlots[i].ClearItemSlot();
            }
        }
    }
}
