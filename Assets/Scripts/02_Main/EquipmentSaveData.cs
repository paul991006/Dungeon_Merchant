using System;
using System.Collections.Generic;

[System.Serializable]
public class EquipmentSaveData
{
    public List<EquippedItemData> equippedItems = new();
}

[System.Serializable]
public class EquippedItemData
{
    public ItemType itemType;
    public ItemInstance item;
}
