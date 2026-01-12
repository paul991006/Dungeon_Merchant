using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<EquipmentSlotUI> slots;

    void Awake()
    {
        Instance = this;
    }

    public void EquipItem(ItemData data, ItemInstance instance)
    {
        EquipmentSlotUI slot = slots.Find(s => s.slotType == data.itemType);
        if (slot == null) return;

        slot.Equip(data, instance);
        instance.isEquipped = true;
    }

    public void Unequip(ItemType type)
    {
        EquipmentSlotUI slot = slots.Find(s => s.slotType == type);
        if (slot == null) return;

        slot.Unequip();
    }
}
