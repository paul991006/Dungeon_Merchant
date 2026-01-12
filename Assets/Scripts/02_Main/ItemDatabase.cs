using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> items;

    Dictionary<int, ItemData> itemDict;

    public void Init()
    {
        itemDict = new Dictionary<int, ItemData>();
        foreach (var item in items)
        {
            if (item == null) continue;
            itemDict[item.itemId] = item;
        }
    }

    public ItemData GetItem(int id)
    {
        if (itemDict == null) Init();

        itemDict.TryGetValue(id, out var data);
        return data;
    }
}
