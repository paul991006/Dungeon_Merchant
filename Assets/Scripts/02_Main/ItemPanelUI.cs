using UnityEngine;
using UnityEngine.UI;

public class ItemPanelUI : MonoBehaviour
{
    public static ItemPanelUI Instance;

    public Image itemImage;
    public Text itemNameText;
    public Text itemTypeText;
    public Text statText;

    private ItemData currentData;
    private ItemInstance currentInstance;

    void Awake()
    {
        if (Instance == null) Instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
    }

    public void Show(ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) return;

        currentData = data;
        currentInstance = instance;

        gameObject.SetActive(true);

        itemImage.sprite = data.icon;
        itemNameText.text = $"{data.itemName} +{instance.enhance}";
        itemTypeText.text = data.itemType.ToString();

        statText.text = $"공격력 : {instance.attack}\n" + $"방어력 : {instance.defense}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClickEquip()
    {
        EquipmentManager.Instance.EquipItem(currentData, currentInstance);
        InventoryManager.Instance.RefreshUI();
    }

}
