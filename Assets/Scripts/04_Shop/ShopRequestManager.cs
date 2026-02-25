using UnityEngine;
using UnityEngine.UI;

public class ShopRequestManager : MonoBehaviour
{
    public static ShopRequestManager Instance;

    public ShopRequestCondition currentRequest;
    public ShopInventoryUI shopInventoryUI;

    private ItemInstance selectedItem;
    private int selectedPrice;

    public GameObject confirmPanel;
    public Button acceptButton;
    public Button cancelButton;
    public Text descriptionText;
    public Text previewPriceText;
    public Text infoText;
    public float requestInterval = 600f;
    float timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateNewRequest();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= requestInterval)
        {
            GenerateNewRequest();
            timer = 0f;
        }
    }

    void GenerateNewRequest()
    {
        Debug.Log("새로운 상점 요청이 생성되었습니다.");
        currentRequest = new ShopRequestCondition();
        currentRequest.requiredType = (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length);
        currentRequest.minGrade = (ItemGrade)Random.Range(0, System.Enum.GetValues(typeof(ItemGrade)).Length);
        currentRequest.minDurability = (ItemDurability)Random.Range(0, System.Enum.GetValues(typeof(ItemDurability)).Length);
        currentRequest.priceMultiplier = Random.Range(1.0f, 1.5f);
        descriptionText.text = currentRequest.GetConditionDescription();
    }

    public void OnSelectItem(ItemInstance item)
    {
        if (currentRequest == null) return;

        confirmPanel.SetActive(true);

        if (currentRequest.IsMatch(item))
        {
            selectedItem = item;
            selectedPrice = Mathf.RoundToInt(item.originPrice * currentRequest.priceMultiplier);

            previewPriceText.text = $"이 아이템을 {selectedPrice}G에 판매하시겠습니까?"; 

            acceptButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
        }
        else
        {
            previewPriceText.text = "이 아이템은 조건에 맞지 않는 아이템입니다!";
            acceptButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }
    }

    public void OnClickSell()
    {
        if (selectedItem == null) return;

        InventoryManager.Instance.RemoveItem(selectedItem);
        PlayerData.Instance.AddGold(selectedPrice);

        shopInventoryUI.inventoryUI.RefreshUI();

        selectedItem = null;
    }

    public void OnClickAccept()
    {
        shopInventoryUI.Open(currentRequest);
    }
}
