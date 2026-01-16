using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public ItemType slotType;
    public Image iconImage;
    public Button button;

    public ItemInstance CurrentItem;
    public ItemData CurrentData;

    void Awake()
    {
        button.onClick.AddListener(OnClick);
        SetEmpty();
    }

    public void Equip(ItemData data, ItemInstance instance)
    {
        CurrentData = data;
        CurrentItem = instance;

        iconImage.sprite = data.icon;
        iconImage.enabled = true;
    }

    public void Unequip()
    {
        if (CurrentItem != null) CurrentItem.isEquipped = false;
        SetEmpty();
    }

    void SetEmpty()
    {
        CurrentData = null;
        CurrentItem = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
    }

    void OnClick()
    {
        if (CurrentData == null) return;

        ItemPanelUI.Instance.Show(CurrentData, CurrentItem);
    }
}
