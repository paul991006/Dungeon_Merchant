using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public ItemType slotType;
    public Image iconImage;
    public Button button;

    private ItemInstance equippedInstance;
    private ItemData equippedData;

    void Awake()
    {
        button.onClick.AddListener(OnClick);
        SetEmpty();
    }

    public void Equip(ItemData data, ItemInstance instance)
    {
        equippedData = data;
        equippedInstance = instance;

        iconImage.sprite = data.icon;
        iconImage.enabled = true;
    }

    public void Unequip()
    {
        SetEmpty();
    }

    void SetEmpty()
    {
        equippedData = null;
        equippedInstance = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
    }

    void OnClick()
    {
        if (equippedData == null) return;

        ItemPanelUI.Instance.Show(equippedData, equippedInstance);
    }
}
