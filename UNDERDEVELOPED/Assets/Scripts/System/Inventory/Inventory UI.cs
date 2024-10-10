using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI _instance;
    
    [SerializeField]
    private Transform _itemSlotsHolder, _equipmentSlotsHolder;
    
    private Inventory _inventory;
    private InventorySlotController[] _itemSlots;
    private InventorySlotController[] _equipmentSlots;

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
        _inventory = Inventory._instance;
        _inventory.onItemChangedCallBack += UpdateInventoryUI;
        _itemSlots = _itemSlotsHolder.GetComponentsInChildren<InventorySlotController>();
        _equipmentSlots = _equipmentSlotsHolder.GetComponentsInChildren<InventorySlotController>();
        UpdateInventoryUI();
    }

    private void Update()
    {
        
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (i < _inventory._items.Count)
            {
                _itemSlots[i].AddItem(_inventory._items[i]);
            }
            else
            {
                _itemSlots[i].ClearItemSlot();
            }
        }

        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (i < _inventory._equipments.Count)
            {
                _equipmentSlots[i].AddItem(_inventory._equipments[i]);
            }
            else
            {
                _equipmentSlots[i].ClearItemSlot();
            }
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        _inventory.onItemChangedCallBack -= UpdateInventoryUI;
    }
}
