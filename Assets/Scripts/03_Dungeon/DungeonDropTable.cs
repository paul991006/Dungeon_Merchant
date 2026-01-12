using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/DropTable")]
public class DungeonDropTable : ScriptableObject
{
    public List<ItemData> dropItems;
}
