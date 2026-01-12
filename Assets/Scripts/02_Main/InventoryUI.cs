using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform content;
    public GameObject itemSlotPrefab;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in content) Destroy(child.gameObject);

        foreach (ItemInstance instance in InventoryManager.Instance.inventory)
        {
            ItemData data = GameManager.Instance.itemDatabase.GetItem(instance.itemId);

            GameObject slot = Instantiate(itemSlotPrefab, content);
            slot.GetComponent<ItemSlotUI>().SetItem(data, instance);
        }
    }
}
