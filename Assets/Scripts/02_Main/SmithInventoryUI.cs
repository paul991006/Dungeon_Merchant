using UnityEngine;

public class SmithInventoryUI : MonoBehaviour
{
    public Transform content;
    public GameObject itemSlotPrefab;

    public ItemPanelMode mode;
    public static SmithInventoryUI Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in content) Destroy(child.gameObject);

        foreach (ItemInstance instance in InventoryManager.Instance.inventory)
        {
            ItemData data = GameManager.Instance.itemDatabase.GetItem(instance.itemId);

            GameObject slot = Instantiate(itemSlotPrefab, content);
            slot.GetComponent<ItemSlotUI>().SetItem(data, instance, mode);
        }
    }
}
