using UnityEngine.UI;
using UnityEngine;

public class InventorySlotController : MonoBehaviour
{
    [SerializeField]
    private Image _itemIcon;
    [SerializeField]
    private GameObject _informationBox;
    public Item _item;

    private void OnEnable()
    {
        _informationBox.SetActive(false);
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

    public void SelectedItem()
    {
        if (_item != null)
        {
            _informationBox.SetActive(false);
            _informationBox.GetComponent<ItemInfoManager>()._currentInventorySlot = this;
            _informationBox.SetActive(true);
            return;
        }

        // Debug.LogError("InventorySlotController: Selected Item is NULL");
    }

    public void DropItem()
    {
        ItemDropCreator.CreateItemDrop(Player._instance.GetPlayerPosition(), _item);
        Inventory._instance.RemoveItem(_item);
    }
}
