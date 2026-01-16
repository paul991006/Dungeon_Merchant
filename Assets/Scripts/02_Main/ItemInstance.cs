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

    public string GetStatDescription()
    {
        string text = "";

        if (attack > 0) text += $"공격력 +{attack}\n";
        if (defense > 0) text += $"방어력 +{defense}\n";
        if (hp > 0) text += $"체력 +{hp}\n";
        if (heal > 0) text += $"회복량 +{heal}\n";

        return text;
    }
}
