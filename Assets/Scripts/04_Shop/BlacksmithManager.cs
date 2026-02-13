using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
    public static BlacksmithManager Instance;

    public GameObject panel;
    public bool isBlacksmithMode = false;

    void Awake()
    {
        Instance = this;
    }

    public void OpenBlacksmith()
    {
        isBlacksmithMode = true;
        panel.SetActive(true);

        if (SmithInventoryUI.Instance != null)
        {
            SmithInventoryUI.Instance.mode = ItemPanelMode.Smith;
            SmithInventoryUI.Instance.RefreshUI();
        }
    }

    public void CloseBlacksmith()
    {
        isBlacksmithMode = false;
        panel.SetActive(false);
    }

    public void TryUpgrade(ItemInstance item)
    {
        if (item == null) return;
        if (item.grade >= ItemGrade.Legendary) return;

        item.grade += 1;
        InventoryManager.Instance?.SaveInventory();

        if (isBlacksmithMode)
        {
            SmithInventoryUI.Instance?.RefreshUI();
        }
        else
        {
            InventoryUI.Instance?.RefreshUI();
        }
    }

    public void TryRepair(ItemInstance item)
    {
        if (item == null) return;
        if (item.durability >= ItemDurability.Perfect) return;

        item.durability += 1;
        InventoryManager.Instance?.SaveInventory();

        if (isBlacksmithMode)
        {
            SmithInventoryUI.Instance?.RefreshUI();
        }
        else
        {
            InventoryUI.Instance?.RefreshUI();
        }
    }
}
