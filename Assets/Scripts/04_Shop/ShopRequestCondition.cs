using System;
using UnityEngine;

[Serializable]
public class ShopRequestCondition
{
    public ItemType requiredType;
    public ItemGrade minGrade;
    public ItemDurability minDurability;

    public float priceMultiplier;

    public bool IsMatch(ItemInstance item)
    {
        if (item.data.itemType != requiredType) return false;
        if (item.grade < minGrade) return false;
        if (item.durability < minDurability) return false;
        return true;
    }

    public string GetConditionDescription()
    {
        int percent = Mathf.RoundToInt((priceMultiplier - 1f) * 100f);

        return  $"등급이 {minGrade}등급 이상이고 상태가 {minDurability} 이상인 " +
                $"{requiredType} 아이템을 가져오면\n원가보다 {percent}% 더 비싸게 구매할게요!";
    }

    public string GetInfoText()
    {
        return $"등급이 {minGrade} 이상이고 " +
           $"상태가 {minDurability} 이상인 " +
           $"{requiredType} 아이템";
    }
}
