using UnityEngine;
using UnityEngine.UI;

public class RewardItemSlotUI : MonoBehaviour
{
    public Image background;
    public Image icon;

    public void Set(RewardItemResult result)
    {
        icon.sprite = result.item.icon;
        background.color = ItemGradeColorUtil.GetColor(result.grade);
    }
}
