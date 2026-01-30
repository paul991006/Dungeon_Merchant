using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ItemInstance> inventory = new List<ItemInstance>();

    const string SAVE_KEY = "INVENTORY_DATA";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    IEnumerator Start()
    {
        yield return null;
        LoadInventory();
    }

    public void AddItem(ItemData data, ItemGrade grade)
    {
        ItemDurability dur = RollDurability();

        ItemInstance instance = new ItemInstance
        {
            itemId = data.itemId,
            isEquipped = false,
            grade = grade,
            durability = dur,

            attack = RollStat(data.baseAttack, grade, dur),
            defense = RollStat(data.baseDefense, grade, dur),
            hp = RollStat(data.baseHp, grade, dur),
            heal = RollStat(data.baseHeal, grade, dur)
        };
        instance.basePrice = CalculatePrice(data, instance);

        inventory.Add(instance);
        SaveInventory();
    }

    public void RemoveItem(ItemInstance instance)
    {
        inventory.Remove(instance);
        SaveInventory();
    }

    ItemDurability RollDurability()
    {
        int roll = Random.Range(0, 100);

        if (roll < 10) return ItemDurability.Trash;       // 10%
        if (roll < 25) return ItemDurability.VeryBad;     // 15%
        if (roll < 45) return ItemDurability.Bad;         // 20%
        if (roll < 70) return ItemDurability.Normal;      // 25%
        if (roll < 85) return ItemDurability.Good;        // 15%
        if (roll < 95) return ItemDurability.VeryGood;    // 10%
        return ItemDurability.Perfect;                    // 5%
    }

    void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();
        saveData.items = inventory;

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    void LoadInventory()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY)) return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);
        inventory = saveData.items;
    }

    float GetGradeMultiplier(ItemGrade grade)
    {
        switch (grade)
        {
            case ItemGrade.Common: return 0.6f;
            case ItemGrade.Rare: return 1.0f;
            case ItemGrade.Epic: return 1.3f;
            case ItemGrade.Unique: return 1.6f;
            case ItemGrade.Legendary: return 2f;
            default: return 1f;
        }
    }

    public static float GetGradePriceMultiplier(ItemGrade grade)
    {
        switch (grade)
        {
            case ItemGrade.Common: return 0.5f;   
            case ItemGrade.Rare: return 1.0f;
            case ItemGrade.Epic: return 1.5f;     
            case ItemGrade.Unique: return 2.0f;  
            case ItemGrade.Legendary: return 4.0f; 
            default: return 1f;
        }
    }

    float GetDurabilityMultiplier(ItemDurability durability)
    {
        switch (durability)
        {
            case ItemDurability.Trash: return 0.4f;
            case ItemDurability.VeryBad: return 0.6f;
            case ItemDurability.Bad: return 0.8f;
            case ItemDurability.Normal: return 1.0f;
            case ItemDurability.Good: return 1.2f;
            case ItemDurability.VeryGood: return 1.4f;
            case ItemDurability.Perfect: return 2f;
            default: return 1f;
        }
    }

    public static float GetDurabilityPriceMultiplier(ItemDurability durability)
    {
        switch (durability)
        {
            case ItemDurability.Trash: return 0.2f;
            case ItemDurability.VeryBad: return 0.5f;
            case ItemDurability.Bad: return 0.8f;
            case ItemDurability.Normal: return 1.0f;
            case ItemDurability.Good: return 1.5f;
            case ItemDurability.VeryGood: return 2.0f;
            case ItemDurability.Perfect: return 4.0f;
            default: return 1f;
        }
    }

    int RollStat(int baseValue, ItemGrade grade, ItemDurability durability)
    {
        float gradeMul = GetGradeMultiplier(grade);
        float durMul = GetDurabilityMultiplier(durability);
        float rand = Random.Range(0.7f, 1.3f);

        return Mathf.RoundToInt(baseValue * gradeMul * durMul * rand);
    }

    public static int CalculatePrice (ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) return 0;
        float price = data.basePrice;
        price *= GetGradePriceMultiplier(instance.grade);
        price *= GetDurabilityPriceMultiplier(instance.durability);
        price *= Random.Range(0.7f, 1.3f);
        return Mathf.Max(1, Mathf.RoundToInt(price));
    }

    public void AddItemInstance(ItemInstance instance)
    {
        if (inventory.Contains(instance)) return;
        instance.isEquipped = false;
        inventory.Add(instance);
        SaveInventory();
    }
}