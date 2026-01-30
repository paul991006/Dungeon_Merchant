using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image background;
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
        icon.color = Color.white;

        icon.preserveAspect = true;

        SetGradeColor(instance.grade);

        if (mode == ItemPanelMode.Inventory)
        {
            //인벤토리에서는 항상 표시
            icon.enabled = true;
        }
        else if (mode == ItemPanelMode.Equipment)
        {
            //장비창 전용 처리
            icon.enabled = true;
        }
    }

    public void Clear()
    {
        data = null;
        instance = null;

        icon.sprite = null;
        icon.color = Color.white;

        background.color = Color.white;
    }

    void SetGradeColor(ItemGrade grade)
    {
        background.color = ItemGradeColorUtil.GetColor(grade);
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
        if (ItemTooltipUI.Instance != null) ItemTooltipUI.Instance.Hide();
        if (mode == ItemPanelMode.Storage)
        {
            if (HalfShopPanelUI.Instance != null)
            {
                HalfShopPanelUI.Instance.Show(data, instance);
                return;
            }
        }
        if (ItemPanelUI.Instance == null) return;

        ItemPanelUI.Instance.Show(data, instance, mode);
    }
}
