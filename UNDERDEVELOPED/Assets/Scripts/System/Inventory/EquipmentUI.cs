using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI _instance;
    [SerializeField]
    private Transform _equipmentSlotsHolder;
    private EquipmentManager _equipmentManager;
    private InventorySlotController[] _equipmentSlots;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        EquipmentManager.onEquipmentChanged += UpdateEquipmentUI;
    }

    private void Start()
    {
        _equipmentManager = EquipmentManager._instance;
        _equipmentSlots = _equipmentSlotsHolder.GetComponentsInChildren<InventorySlotController>();
        UpdateEquipmentUI(null, null);
    }

    private void UpdateEquipmentUI(Equipment newITem, Equipment oldITem)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentManager.GetCurrentEquipments()[i] != null)
            {
                _equipmentSlots[i].AddItem(_equipmentManager.GetCurrentEquipments()[i]);
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

        EquipmentManager.onEquipmentChanged -= UpdateEquipmentUI;
    }
}
