using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon;
    public ItemType slotType;

    public ItemInstance CurrentItem;
    public ItemData CurrentData;

    void Awake()
    {
        SetEmpty();
    }

    public void Equip(ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) return;

        CurrentData = data;
        CurrentItem = instance;

        icon.sprite = data.icon;
        icon.color = Color.white;
        icon.raycastTarget = true;
    }

    public void Unequip()
    {
        SetEmpty();
    }

    void SetEmpty()
    {
        CurrentData = null;
        CurrentItem = null;

        icon.sprite = null;
        icon.color = new Color(1, 1, 1, 0); //≈ı∏Ì
        icon.raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CurrentItem == null || CurrentData == null) return;

        ItemTooltipUI.Instance.Show(CurrentData, CurrentItem, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltipUI.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CurrentItem == null || CurrentData == null) return;

        ItemPanelUI.Instance.Show(CurrentData, CurrentItem, ItemPanelMode.Equipment);
    }
}
