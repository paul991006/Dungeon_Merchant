using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    void Start()
    {
        int stage = DungeonSelectionData.Stage;
        int level = DungeonSelectionData.Level;

        Debug.Log($"Dungeon Start : {stage}-{level}");

        SetupDungeon(stage, level);
    }

    void SetupDungeon(int stage, int level)
    {
        // 몬스터 스폰
        // 난이도 배율
        // 보상 테이블
    }
}
