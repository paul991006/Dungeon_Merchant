using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public int itemId;
    public string itemName;
    public ItemType itemType;
    public Sprite icon;

    public int baseHp;
    public int baseHeal;
    public int baseAttack;
    public int baseDefense;
    public int basePrice;
}
