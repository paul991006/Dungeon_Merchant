using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Transform content;
    public GameObject itemSlotPrefab;

    public List<ItemInstance> inventory = new List<ItemInstance>();

    const string SAVE_KEY = "INVENTORY_DATA";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    IEnumerator Start()
    {
        yield return null;
        LoadInventory();
    }

    public void AddItem(ItemData data)
    {
        ItemInstance instance = new ItemInstance
        {
            itemId = data.itemId,
            enhance = 0,
            isEquipped = false,

            attack = RollStat(data, data.Attack),
            defense = RollStat(data, data.Defense)
        };

        inventory.Add(instance);
        SaveInventory();
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in content) Destroy(child.gameObject);

        foreach (ItemInstance instance in inventory)
        {
            ItemData data = GameManager.Instance.itemDatabase.GetItem(instance.itemId);

            GameObject slot = Instantiate(itemSlotPrefab, content);
            slot.GetComponent<ItemSlotUI>().SetItem(data, instance);
        }
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
        RefreshUI();
    }

    float GetGradeMultiplier(ItemGrade grade)
    {
        switch (grade)
        {
            case ItemGrade.Common: return 1.0f;
            case ItemGrade.Rare: return 1.1f;
            case ItemGrade.Epic: return 1.25f;
            case ItemGrade.Unique: return 1.35f;
            case ItemGrade.Legendary: return 1.5f;
            default: return 1f;
        }
    }

    int RollStat(ItemData data, int baseValue)
    {
        float gradeMul = GetGradeMultiplier(data.grade);
        float rand = Random.Range(0.7f, 1.3f);
        return Mathf.RoundToInt(baseValue * gradeMul * rand);
    }
}