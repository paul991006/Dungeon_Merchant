using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<EquipmentSlotUI> slots;

    private Dictionary<ItemType, ItemInstance> equippedItems = new();
    const string SAVE_KEY = "EQUIPMENT_DATA";

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this; 
        } 
        else Destroy(gameObject);
    }

    private void Start()
    {
        Load();
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

        EquipmentSlotUI slot = slots.Find(s => s.slotType == type);
        slot?.Equip(data, instance);

        Save();
    }

    public void Unequip(ItemType type)
    {
        if (!equippedItems.ContainsKey(type)) return;

        ItemInstance instance = equippedItems[type];
        instance.isEquipped = false;

        InventoryManager.Instance.AddItemInstance(instance);

        EquipmentSlotUI slot = slots.Find(s => s.slotType == type);
        slot?.Unequip();

        equippedItems.Remove(type);
        Save();
    }
    
    void Save()
    {
        EquipmentSaveData save = new();

        foreach (var pair in equippedItems) 
        {
            save.equippedItems.Add(new EquippedItemData
            {
                itemType = pair.Key,
                item = pair.Value
            });
        }

        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(save));
        PlayerPrefs.Save();
    }

    void Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY)) return;

        EquipmentSaveData save = JsonUtility.FromJson<EquipmentSaveData>(PlayerPrefs.GetString(SAVE_KEY));

        foreach (var e in save.equippedItems)
        {
            equippedItems[e.itemType] = e.item;
            e.item.isEquipped= true;

            ItemData data = GameManager.Instance.itemDatabase.GetItem(e.item.itemId);
            EquipmentSlotUI slot = slots.Find(s => s.slotType==e.itemType);
            slot?.Equip(data, e.item);
        }
    }
}
