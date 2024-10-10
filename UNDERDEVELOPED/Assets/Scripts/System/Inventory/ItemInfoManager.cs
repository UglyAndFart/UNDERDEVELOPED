using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoManager : MonoBehaviour
{
    public InventorySlotController _currentInventorySlot;
    private Item _item;

    [Header("Item Information Box")]
    [SerializeField]
    private GameObject _informationBox;
    [SerializeField]
    private Image _infoItemIcon;
    [SerializeField]
    private TextMeshProUGUI _infoItemName, _infoItemDescription;
    // [SerializeField]
    // private Button _useButton, _equipButton, _dropButton;

    private void OnEnable()
    {   
        _item = _currentInventorySlot._item;

        Debug.Log("ItemInfoManager: Enabled" + _item._icon);
        _infoItemIcon.sprite = _item._icon;
        _infoItemName.text = _item._name;
        _infoItemDescription.text = RetrieveDescription();

        // if (_item is Equipment)
        // {
        //     _equipButton.interactable = true;
        //     _useButton.interactable = false;
        // }
        // else if (_item is Item)
        // {
        //     _equipButton.interactable = false;
        //     _useButton.interactable = true;
        // }
    }

    // If description length is more than 1 means its an equipment
    // Format the string accordingly then return it
    // return "" when if the _description length is 0
    private string RetrieveDescription()
    {
        string[] description = _item._description;

        if (description.Length == 1)
        {
            return description[0].ToString();
        }
        else if (description.Length == 0)
        {
            return "";
        }
        
        return $"{description[0]}\n\nWeapon Type: {description[1]}"
            + $"\nPhysical Damage: {description[2]}\nMagic Damage: {description[3]}";
    }

    private void ResetValues()
    {
        _currentInventorySlot = null;
        _item = null;
        _infoItemIcon.sprite = null;
        _infoItemName.text = null;
        _infoItemDescription.text = null;
    }

    public void UseItem()
    {
        _item.UseItem();
    }

    public void UnequipItem()
    {
        EquipmentManager._instance.UnequipItem((int)((Equipment) _item)._equipmentSlot);
    }

    public void DropItem()
    {
        _currentInventorySlot.DropItem();
        ResetValues();
    }
}
