using UnityEngine;
using UnityEngine.UI;

public class ItemPanelUI : MonoBehaviour
{
    public static ItemPanelUI Instance;

    public GameObject panel;
    public Text msg;
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
        if (data == null || instance == null)
        {
            close();
            return;
        }

        currentData = data;
        currentInstance = instance;
        currentMode = mode;
 
        panel.SetActive(true);    
        ItemTooltipUI.Instance.Hide();

        UpdateUIByMode();
    }

    void UpdateUIByMode()
    {
        switch (currentMode)
        {
            case ItemPanelMode.Equipment:
                msg.text = "장착된 장비를 해제하시겠습니까?";
                equipBtn.gameObject.SetActive(false);
                unequipBtn.gameObject.SetActive(true);
                break;
            case ItemPanelMode.Inventory:
                msg.text = "해당 아이템을 장착하시겠습니까?";
                equipBtn.gameObject.SetActive(true);
                unequipBtn.gameObject.SetActive(false);
                break;
        }
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

    public void close()
    {
        panel.SetActive(false);
    }
}
