using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string _name = "New Item";
    public Sprite _icon = null;
    public string[] _description;
    public bool _defaultItem = false;

    public virtual bool UseItem()
    {
        return true;
    }
}
