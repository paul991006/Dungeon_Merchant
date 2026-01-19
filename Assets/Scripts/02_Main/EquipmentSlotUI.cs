using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ItemType slotType;
    public Transform itemRoot;
    public GameObject itemSlotPref;

    public ItemSlotUI currentSlot;
    public ItemInstance CurrentItem;
    public ItemData CurrentData;

    public void Equip(ItemData data, ItemInstance instance)
    {
        Clear();

        CurrentItem = instance;
        CurrentData = data;

        GameObject go = Instantiate(itemSlotPref, itemRoot);
        currentSlot = go.GetComponent<ItemSlotUI>();
        currentSlot.SetItem(data, instance, ItemPanelMode.Equipment);
    }

    public void Unequip()
    {
        Clear();
    }

    public void Clear()
    {
        if (currentSlot != null) Destroy(currentSlot.gameObject);

        currentSlot = null;
        CurrentItem = null;
        CurrentData = null;
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
