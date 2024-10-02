using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class InventorySlotController : MonoBehaviour
{
    [SerializeField]
    private Image _itemIcon;
    private Item _item;
    
    [Header("Item Information Box")]
    [SerializeField]
    private GameObject _informationBox;
    [SerializeField]
    private Image _infoItemIcon;
    [SerializeField]
    private TextMeshProUGUI _infoItemName;
    [SerializeField]
    private TextMeshProUGUI _infoItemDescription;

    private void OnEnable()
    {
        _informationBox.SetActive(false);
        Debug.Log("Biggaasdasdasdasdasssssssssssssssssssssssssssssssssssssssssssssssss");
    }

    public void AddItem(Item newItem)
    {
        _item = newItem;

        _itemIcon.sprite = newItem._icon;
        _itemIcon.enabled = true;
    }

    public void ClearItemSlot()
    {
        _item = null;

        _itemIcon.sprite = null;
        _itemIcon.enabled = false;
    }

    public void ItemSelected()
    {
        if (_item != null)
        {
            _informationBox.SetActive(true);
            _infoItemIcon.sprite = _item._icon;
            _infoItemName.text = _item._name;
            _infoItemDescription.text = RetrieveDescription();
        } 
    }

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
}
