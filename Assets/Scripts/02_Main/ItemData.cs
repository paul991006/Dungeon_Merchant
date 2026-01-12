using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public int itemId;
    public string itemName;
    public ItemType itemType;
    public ItemGrade grade;
    public Sprite icon;

    public int baseHp;
    public int baseHeal;
    public int baseAttack;
    public int baseDefense;

    public int Attack => Mathf.RoundToInt(baseAttack * ItemGradeMultiplier.Get(grade));
    public int Defense => Mathf.RoundToInt(baseDefense * ItemGradeMultiplier.Get(grade));
    public int Hp => Mathf.RoundToInt(baseHp * ItemGradeMultiplier.Get(grade));
    public int Heal => Mathf.RoundToInt(baseHeal * ItemGradeMultiplier.Get(grade));
}
