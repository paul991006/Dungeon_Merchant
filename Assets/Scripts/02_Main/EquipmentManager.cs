using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<EquipmentSlotUI> slots;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void EquipItem(ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) return;

        EquipmentSlotUI slot = slots.Find(s => s != null && s.slotType == data.itemType);
        
        if (slot == null) return;

        slot.Unequip();

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
