using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon;
    public ItemType slotType;

    public ItemInstance CurrentItem;
    public ItemData CurrentData;

    public void Equip(ItemData data, ItemInstance instance)
    {
        CurrentData = data;
        CurrentItem = instance;

        icon.sprite = data.icon;
        icon.enabled = true;
    }

    public void Unequip()
    {
        CurrentData = null;
        CurrentItem = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CurrentItem == null) return;

        ItemTooltipUI.Instance.Show(CurrentData, CurrentItem, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltipUI.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CurrentItem == null) return;

        ItemPanelUI.Instance.Show(CurrentData, CurrentItem, ItemPanelMode.Equipment);
    }
}
