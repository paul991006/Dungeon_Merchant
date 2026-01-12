using UnityEngine;
using UnityEngine.UI;

public class ItemPanelUI : MonoBehaviour
{
    public static ItemPanelUI Instance { get; private set; }

    public Image itemImage;
    public Text itemNameText;
    public Text itemTypeText;
    public Text statText;
    public GameObject panel;

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
    }

    public void Show(ItemData data, ItemInstance instance)
    {
        currentData = data;
        currentInstance = instance;

        panel.SetActive(true);

        itemImage.sprite = data.icon;
        itemNameText.text = data.itemName;
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
    }
}
