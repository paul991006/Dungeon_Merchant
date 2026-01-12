using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;

    ItemData data;
    ItemInstance instance;

    public void SetItem(ItemData data, ItemInstance instance)
    {
        this.data = data;
        this.instance = instance;
        icon.sprite = data.icon;
    }

    public void OnClick()
    {
        ItemPanelUI.Instance.Show(data, instance);
    }
}
