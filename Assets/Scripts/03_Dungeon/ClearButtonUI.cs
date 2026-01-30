using UnityEngine;

public class ClearButtonUI : MonoBehaviour
{
    public void OnClickClear()
    {
        DungeonClearManager.Instance.ClearDungeon();
    }
}
