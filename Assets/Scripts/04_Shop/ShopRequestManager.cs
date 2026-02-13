using UnityEngine;
using UnityEngine.UI;

public class ShopRequestManager : MonoBehaviour
{
    public ShopRequestCondition currentRequest;

    public Text descriptionText;
    public Text previewPriceText;
    public float requestInterval = 600f;
    float timer;

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
        currentRequest = new ShopRequestCondition();
        currentRequest.requiredType = (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length);
        currentRequest.minGrade = (ItemGrade)Random.Range(0, System.Enum.GetValues(typeof(ItemGrade)).Length);
        currentRequest.minDurability = (ItemDurability)Random.Range(0, System.Enum.GetValues(typeof(ItemDurability)).Length);
        currentRequest.priceMultiplier = Random.Range(1.0f, 1.5f);
        descriptionText.text = currentRequest.GetConditionDescription();
    }

    public bool TrySell(ItemInstance item, out int finalPrice)
    {
        finalPrice = item.originPrice;

        if (currentRequest == null) return false;

        if (currentRequest.IsMatch(item))
        {
            finalPrice = Mathf.RoundToInt(item.originPrice * currentRequest.priceMultiplier);
            GenerateNewRequest();
            timer = 0f;
            return true;
        }

        return false;
    }

    public void OnSelectItem(ItemInstance item)
    {
        if (currentRequest == null) return;

        if (currentRequest.IsMatch(item))
        {
            int finalPrice = Mathf.RoundToInt(item.originPrice * currentRequest.priceMultiplier);
            previewPriceText.text = $"이 아이템을 {finalPrice}G에 판매하시겠습니까?"; 
        }
        else
        {
            previewPriceText.text = "이 아이템은 조건에 맞지 않는 아이템입니다!";
        }
    }
}
