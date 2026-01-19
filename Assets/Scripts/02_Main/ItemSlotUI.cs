using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon;

    private ItemData data;
    private ItemInstance instance;
    private ItemPanelMode mode;

    public void SetItem(ItemData data, ItemInstance instance, ItemPanelMode mode)
    {
        if (data == null || instance == null)
        {
            Clear();
            return;
        }

        this.data = data;
        this.instance = instance;
        this.mode = mode;

        icon.sprite = data.icon;
        icon.enabled = true;
    }

    public void Clear()
    {
        data = null;
        instance = null;

        icon.sprite = null;
        icon.enabled = false;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (data == null || instance == null) return;
        ItemTooltipUI.Instance.Show(data, instance, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ItemTooltipUI.Instance == null) return;
        ItemTooltipUI.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (data == null || instance == null) return;

        ItemPanelUI.Instance.Show(data, instance, mode);
    }
}
