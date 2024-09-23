using UnityEngine.UI;
using UnityEngine;

public class InventorySlotController : MonoBehaviour
{
    [SerializeField]
    private Image _itemIcon;
    private Item _item;

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
}
