using UnityEngine;
using UnityEngine.UI;

public class ItemPanelUI : MonoBehaviour
{
    public static ItemPanelUI Instance;

    public GameObject panel;
    public Button equipBtn;
    public Button unequipBtn;

    private ItemData currentData;
    private ItemInstance currentInstance;
    private ItemPanelMode currentMode;

    public bool IsOpen => panel.activeSelf;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(ItemData data, ItemInstance instance, ItemPanelMode mode)
    {
        currentData = data;
        currentInstance = instance;
        currentMode = mode;

        equipBtn.gameObject.SetActive(mode == ItemPanelMode.Inventory);
        unequipBtn.gameObject.SetActive(mode == ItemPanelMode.Equipment);
        
        panel.SetActive(true);
        ItemTooltipUI.Instance.Hide();
    }

    public void OnClickEquip()
    {
        EquipmentManager.Instance.EquipItem(currentData, currentInstance);
        close();
    }

    public void OnClickUnequip() 
    {
        EquipmentManager.Instance.Unequip(currentData.itemType);
        close();
    }

    void close()
    {
        panel.SetActive(false);
    }
}
