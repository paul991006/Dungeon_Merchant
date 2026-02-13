using System;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public int itemId;
    public bool isEquipped;

    public ItemData data;
    public ItemGrade grade;
    public ItemDurability durability;

    public float priceSeed;
    public float statSeed;
    public int originPrice;
    
    public ItemInstance(ItemData data, ItemGrade grade, ItemDurability durability)
    {
        this.data = data;
        this.itemId = data.itemId;
        this.grade = grade;
        this.durability = durability;

        isEquipped = false; 

        priceSeed = UnityEngine.Random.Range(0.7f, 1.3f);
        statSeed = UnityEngine.Random.Range(0.7f, 1.3f);
        originPrice = Mathf.Max(1, Mathf.RoundToInt(data.basePrice * priceSeed));
    }

    public int attack => CalculateStat(data.baseAttack);
    public int defense => CalculateStat(data.baseDefense);
    public int hp => CalculateStat(data.baseHp);
    public int heal => CalculateStat(data.baseHeal);

    int CalculateStat(int baseStat)
    {
        if (baseStat <= 0) return 0;

        float stat = baseStat;
        stat *= statSeed;
        stat *= InventoryManager.GetGradeMultiplier(grade);
        stat *= InventoryManager.GetDurabilityMultiplier(durability);

        return Mathf.Max(1, Mathf.RoundToInt(stat));
    }

    public string GetStatDescription()
    {
        string text = "";

        if (attack > 0) text += $"공격력 +{attack}\n";
        if (defense > 0) text += $"방어력 +{defense}\n";
        if (hp > 0) text += $"체력 +{hp}\n";
        if (heal > 0) text += $"회복량 +{heal}\n";

        return text;
    }

    public string GetDurabilityText()
    {
        switch (durability)
        {
            case ItemDurability.Trash: return "<color=#777777>쓰레기</color>";
            case ItemDurability.VeryBad: return "<color=#888888>매우 나쁨</color>";
            case ItemDurability.Bad: return "<color=#AAAAAA>나쁨</color>";
            case ItemDurability.Normal: return "<color=#FFFFFF>보통</color>";
            case ItemDurability.Good: return "<color=#4CAF50>좋음</color>";
            case ItemDurability.VeryGood: return "<color=#2196F3>매우 좋음</color>";
            case ItemDurability.Perfect: return "<color=#FFD700>완벽함</color>";
            default: return durability.ToString();
        }
    }
}
