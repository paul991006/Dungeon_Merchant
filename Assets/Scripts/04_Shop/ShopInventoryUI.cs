using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryUI : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public Text conditionText;

    ShopRequestCondition currentCondition;

    public void Open(ShopRequestCondition condition)
    {
        currentCondition = condition;

        gameObject.SetActive(true);

        if (inventoryUI != null) inventoryUI.RefreshUI();

        inventoryUI.mode = ItemPanelMode.Sell;

        UpdateConditionText();
    }

    void UpdateConditionText()
    {
        if (currentCondition == null)
        {
            conditionText.text = "";
            return;
        }

        conditionText.text = currentCondition.GetInfoText();
    }
}