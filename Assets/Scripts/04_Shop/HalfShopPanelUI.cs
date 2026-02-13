using UnityEngine;
using UnityEngine.UI;

public class HalfShopPanelUI : MonoBehaviour
{
    public static HalfShopPanelUI Instance;

    public GameObject panel;
    public Text msg;
    public Button sellBtn;

    private ItemData currentData;
    private ItemInstance currentInstance;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(ItemData data, ItemInstance instance)
    {
        if (data == null || instance == null) 
        { 
            panel.SetActive(false);
            return;
        }

        currentData = data;
        currentInstance = instance;

        panel.SetActive(true);

        int halfPrice = Mathf.Max(1, InventoryManager.CalculateFinalPrice(instance, 0.5f));
        msg.text = $"이 아이템을 반값 {halfPrice}G에 판매하시겠습니까?";

        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(SellItem);
    }

    void SellItem()
    {
        int price = Mathf.Max(1, InventoryManager.CalculateFinalPrice(currentInstance, 0.5f));
        PlayerData.Instance.AddGold(price);
        InventoryManager.Instance.RemoveItem(currentInstance);
        InventoryUI.Instance.RefreshUI();
        panel.SetActive(false);
    }
}
