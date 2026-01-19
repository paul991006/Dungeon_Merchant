using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<EquipmentSlotUI> slots;

    private Dictionary<ItemType, ItemInstance> equippedItems = new();

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
        else Destroy(gameObject);
    }

    void Start()
    {
        EquipmentManager.Instance.InitFromPlayerData();
    }

    public void EquipItem(ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) return;

        InventoryManager.Instance.RemoveItem(instance);

        ItemType type = data.itemType;

        // 기존 장비 해제
        if (equippedItems.ContainsKey(type)) Unequip(type);

        equippedItems[type] = instance;
        instance.isEquipped = true;

        PlayerData.Instance.equippedItems.RemoveAll(e => e.itemType == type);
        PlayerData.Instance.equippedItems.Add(new EquippedItemData
        {
            itemType = type,
            item = instance
        });

        PlayerData.Instance.SaveEquipment();

        EquipmentSlotUI slot = GetSlot(type);
        slot?.Equip(data, instance);

        InventoryUI.Instance?.RefreshUI();
    }

    public void Unequip(ItemType type)
    {
        if (!equippedItems.ContainsKey(type)) return;

        ItemInstance instance = equippedItems[type];
        instance.isEquipped = false;

        // 인벤토리로 복귀 (같은 인스턴스!)
        InventoryManager.Instance.AddItemInstance(instance);

        equippedItems.Remove(type);

        // PlayerData에서도 제거
        PlayerData.Instance.equippedItems.RemoveAll(e => e.itemType == type);
        PlayerData.Instance.SaveEquipment();

        // UI
        EquipmentSlotUI slot = GetSlot(type);
        slot?.Clear();

        InventoryUI.Instance?.RefreshUI();
    }

    EquipmentSlotUI GetSlot(ItemType type)
    {
        return slots.Find(s => s.slotType == type);
    }

    public void InitFromPlayerData()
    {
        equippedItems.Clear();

        foreach (var e in PlayerData.Instance.equippedItems)
        {
            equippedItems[e.itemType] = e.item;
            e.item.isEquipped = true;

            ItemData data =
                GameManager.Instance.itemDatabase.GetItem(e.item.itemId);

            EquipmentSlotUI slot = GetSlot(e.itemType);
            slot?.Equip(data, e.item);
        }
    }
}
