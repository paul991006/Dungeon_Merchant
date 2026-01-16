using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<EquipmentSlotUI> slots;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        RestoreEquippedItems();
    }

    void RestoreEquippedItems()
    {
        foreach (var equip in PlayerData.Instance.equippedItems)
        {
            ItemData data = GameManager.Instance.itemDatabase.GetItem(equip.itemInstance.itemId);

            EquipmentSlotUI slot = slots.Find(s => s.slotType == equip.itemType);

            if (data == null || slot == null) continue;

            slot.Equip(data, equip.itemInstance);
            equip.itemInstance.isEquipped = true;
        }
    }


    public void EquipItem(ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) return;

        EquipmentSlotUI slot = slots.Find(s => s != null && s.slotType == data.itemType);
        
        if (slot == null) return;

        if (slot.CurrentItem != null) slot.Unequip();

        slot.Equip(data, instance);
        instance.isEquipped = true;

        SaveEquipData(data.itemType, instance);
    }

    void SaveEquipData(ItemType type, ItemInstance instance)
    {
        var list = PlayerData.Instance.equippedItems;
        list.RemoveAll(e => e.itemType == type);

        list.Add(new EquippedItemData
        {
            itemType = type,
            itemInstance = instance
        });

        PlayerData.Instance.SaveEquipment();
    }

    public void Unequip(ItemType type)
    {
        EquipmentSlotUI slot = slots.Find(s => s.slotType == type);
        if (slot == null) return;

        slot.Unequip();
        PlayerData.Instance.equippedItems.RemoveAll(e => e.itemType == type);
        PlayerData.Instance.SaveEquipment();
    }
}
