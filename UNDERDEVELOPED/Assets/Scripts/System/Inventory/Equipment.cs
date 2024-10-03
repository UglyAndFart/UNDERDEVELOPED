using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot _equipmentSlot;

    public float[] _stats;

    public override bool UseItem()
    {
        EquipmentManager._instance.EquipItem(this);
        OnItemUse();
        return true;
    }

}

public enum EquipmentSlot
{
    weapon,
    foot,
    legs,
    head,
    chest,
    gloves,
    ring
}
