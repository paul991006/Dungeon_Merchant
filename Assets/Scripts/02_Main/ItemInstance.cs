[System.Serializable]
public class ItemInstance
{
    public int itemId;
    public bool isEquipped;

    public int hp;
    public int heal;
    public int attack;
    public int defense;
    public int price;

    public ItemGrade grade;
    public ItemDurability durability;

    public int basePrice;

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
